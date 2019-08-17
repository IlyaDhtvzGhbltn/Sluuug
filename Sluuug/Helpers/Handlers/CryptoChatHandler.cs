using Context;
using Slug.Context;
using Slug.Context.Dto.CryptoConversation;
using Slug.Context.Tables;
using Slug.Helpers.BaseController;
using Slug.Helpers.Handlers.HandlersInterface;
using Slug.Hubs;
using Slug.ImageEdit;
using Slug.Model;
using Slug.Model.Messager.CryptoChat;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SlugAppSettings = System.Web.Configuration.WebConfigurationManager;



namespace Slug.Helpers
{
    public class CryptoChatHandler
    {
        private readonly IDictionary<CryptoChatType, TimeSpan> CryptoChatDestroyDates =
            new Dictionary<CryptoChatType, TimeSpan>()
            {
                { CryptoChatType.Hour, new TimeSpan().Add(new TimeSpan(1,0,0)) },
                { CryptoChatType.Hours_6, new TimeSpan().Add(new TimeSpan(6,0,0)) },
                { CryptoChatType.Day, new TimeSpan().Add(new TimeSpan(1,0,0,0)) },
                { CryptoChatType.Month, new TimeSpan().Add(new TimeSpan(30,0,0,0,0)) },
                { CryptoChatType.Year, new TimeSpan().Add(new TimeSpan(365,0,0,0,0)) },

            };
        private readonly int multiple = int.Parse(SlugAppSettings.AppSettings[AppSettingsEnum.messagesOnPage.ToString()]);

        public CreateNewCryptoChatResponse CreateNewCryptoChat(CryptoChatType type, List<Participant> UserIds, int Inviter)
        {
            var newSecretChat = new SecretChat();
            newSecretChat.CreatorUserId = Inviter;
            newSecretChat.Create = DateTime.Now;
            newSecretChat.Destroy = DateTime.Now.Add(CryptoChatDestroyDates[type]);
            newSecretChat.PartyGUID = Guid.NewGuid();
            using (var context = new DataBaseContext())
            {
                context.SecretChat.Add(newSecretChat);
                context.SaveChanges();
                foreach (var userInvited in UserIds)
                {
                    int userId = Convert.ToInt32(userInvited.UserId);
                    var secretGroups = new SecretChatGroup();
                    secretGroups.PartyGUID = newSecretChat.PartyGUID;
                    secretGroups.UserId = userId;
                    context.SecretChatGroups.Add(secretGroups);
                }
                var secretGroup = new SecretChatGroup();
                secretGroup.PartyGUID = newSecretChat.PartyGUID;
                secretGroup.UserId = Inviter;
                secretGroup.Accepted = true;
                context.SecretChatGroups.Add(secretGroup);
                context.SaveChanges();

                var response = new CreateNewCryptoChatResponse();
                response.CryptoGuidId = newSecretChat.PartyGUID;
                response.ExpireDate = newSecretChat.Destroy;

                return response;
            }
        }
        public CryptoConversationGroupModel GetCryptoChat(string sessionId)
        {
            var userHandler = new UsersHandler();

            var model = new CryptoConversationGroupModel();
            model.CurrentActiveChats = new List<CryptoConversationModel>();
            model.IncommingInviters = new List<CryptoConversationModel>();
            model.OutCommingInviters = new List<CryptoConversationModel>();
            model.FriendsICanInvite = new List<BaseUser>();


            model.FriendsICanInvite = userHandler.GetFriendsOnlyBySession(sessionId, 100);
            var connectionsHandler = new UsersConnectionHandler();
            model.FriendsICanInvite.ForEach(friend => 
            {
                if (connectionsHandler.GetConnectionById(friend.UserId) != null)
                {
                    friend.IsOnline = true;
                }
            });

            int cryptoChatUserId = userHandler.UserIdBySession(sessionId);
            using (var context = new DataBaseContext())
            {
                List<SecretChatGroup> allCryptoChatsGroupsContainsUser = context.SecretChatGroups
                    .Where(x => x.UserId == cryptoChatUserId &&
                    context.DisableDialogs.FirstOrDefault(d =>
                            d.ConversationId == x.PartyGUID &&
                            d.UserDisablerId == cryptoChatUserId) == null)
                    .OrderByDescending(x => x.Id)
                    .ToList();

                foreach (SecretChatGroup chatGroup in allCryptoChatsGroupsContainsUser)
                {

                    SecretChat secretChat = context.SecretChat.FirstOrDefault(x => x.PartyGUID == chatGroup.PartyGUID);
                    if (secretChat != null)
                    {
                        int cryptoChatCreatorUserId = context.SecretChat.First(x => x.PartyGUID == chatGroup.PartyGUID).CreatorUserId;
                        CryptoChatStatus status = ChatStatus(context, chatGroup.UserId, cryptoChatCreatorUserId, secretChat.PartyGUID);

                        var chat = new CryptoConversationModel();
                        chat.GuidId = secretChat.PartyGUID;
                        chat.OpenDate = secretChat.Create;
                        chat.CloseDate = secretChat.Destroy;

                        int isExpired = DateTime.Compare(DateTime.Now, secretChat.Destroy);
                        if (isExpired > 0)
                            chat.Expired = true;
                        else
                        {
                            var remining = chat.CloseDate.Subtract(DateTime.Now);
                            chat.RemainingMins = remining.Minutes;
                            chat.RemainingSecs = remining.Seconds;
                        }



                        int interlocutorID = context.SecretChatGroups
                                            .First(x => 
                                            x.PartyGUID == secretChat.PartyGUID &&
                                            x.UserId != cryptoChatUserId).UserId;

                        BaseUser interlocutor = userHandler.BaseUser(interlocutorID);
                        chat.InterlocutorName = interlocutor.Name;
                        chat.InterlocutorSurName = interlocutor.SurName;
                        chat.InterlocutorAvatar = Resize.ResizedAvatarUri(interlocutor.AvatarResizeUri, ModTypes.c_scale, 100, 100);

                        var GuidId = chatGroup.PartyGUID;
                        var count = context.SecretMessage.Count(x=>x.PartyId == GuidId);
                        if (count > 0)
                        {
                            SecretMessages last = context.SecretMessage.ToList().Last();
                            BaseUser lastMessageSenderInfo = userHandler.BaseUser(last.UserSender);

                            chat.LastMessage = last.Text;
                            chat.LastMessageSendDate = last.SendingDate;
                            chat.LastMessageSenderName = lastMessageSenderInfo.Name;
                            chat.LastMessageSenderSurName = lastMessageSenderInfo.SurName;
                        }


                        if (status == CryptoChatStatus.SelfCreated)
                            model.OutCommingInviters.Add(chat);
                        else if (status == CryptoChatStatus.Accepted)
                            model.CurrentActiveChats.Add(chat);
                        else if (status == CryptoChatStatus.PendingAccepted)
                            model.IncommingInviters.Add(chat);
                    }
                }
            }
            return model;
        }

        public void UpdateAcceptCryptoChat(int userId, Guid chatID)
        {
            using (var context = new DataBaseContext())
            {
                var chatGroup = context.SecretChatGroups.Where(x=>x.UserId == userId && x.PartyGUID == chatID).First();
                chatGroup.Accepted = true;
                context.SaveChanges();
            }
        }

        public async Task SaveSecretMessageHashAsync(Guid ChatId, int UserSenderId, string msgHash)
        {
            using (var context = new DataBaseContext())
            {
                var message = new SecretMessages()
                {
                    PartyId = ChatId,
                    SendingDate = DateTime.UtcNow,
                    UserSender = UserSenderId,
                    Text = msgHash
                };
                context.SecretMessage.Add(message);
                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task<CryptoDialogModel> GetCryptoDialogs(string session, Guid GuidId, int page)
        {
            if (page <= 0)
                page = 1;

            var model = new CryptoDialogModel();
            var usersHandler = new UsersHandler();
            int userReaderID = usersHandler.UserIdBySession(session);

            var userInfos = new Dictionary<int, BaseUser>();

            model.GuidId = GuidId;
            model.Messages = new List<CryptoMessageModel>();
            using (var context = new DataBaseContext())
            {
                var notReadMessage = context.SecretMessage
                    .Where(x =>
                    x.PartyId == GuidId &&
                    x.UserSender != userReaderID)
                    .ToList();

                notReadMessage.ForEach(x =>
                x.IsReaded = true);
                await context.SaveChangesAsync();


                model.isExpired = IsCryptoChatExpired(context, GuidId);
                if (!model.isExpired)
                {
                    var leftTime = CryptoChatTimesLeft(context, GuidId);
                    model.MinsLeft = leftTime.MinsLeft;
                    model.SecLeft = leftTime.SecLeft;
                }


                int multipleCount = context.SecretMessage
                    .Where(x => x.PartyId == GuidId)
                    .Count();
                decimal del = ((decimal)multipleCount / (decimal)this.multiple);
                int resMultiple = Convert.ToInt32(Math.Ceiling(del));
                if (page > resMultiple)
                    page = resMultiple;
                model.PagesCount = resMultiple;

                List<SecretMessages> cryptoMessageCollection = context.SecretMessage
                    .Where(x => x.PartyId == GuidId)
                    .OrderBy(x => x.Id)
                    .Skip((resMultiple - page) * multiple)
                    .Take(multiple)
                    .ToList();

                foreach (var item in cryptoMessageCollection)
                {
                    if (!userInfos.ContainsKey(item.UserSender))
                    {
                        userInfos[item.UserSender] = usersHandler.BaseUser(item.UserSender);
                    }
                    bool incommingFlag = false;
                    if (userReaderID != item.UserSender)
                        incommingFlag = true;

                    var CrMessage = new CryptoMessageModel()
                    {
                        SendDate = item.SendingDate,
                        AvatatURI = Resize.ResizedAvatarUri(userInfos[item.UserSender].AvatarResizeUri, ModTypes.c_scale, 60, 60),
                        Text = item.Text,
                        Name = userInfos[item.UserSender].Name,
                        SurName = userInfos[item.UserSender].SurName,
                        IsIncomming = incommingFlag
                    };
                    model.Messages.Add(CrMessage);
                }
                //}
                //else return null;
            }
            return model;
        }

        public int GetInterlocutorID(Guid cryptoCnvId, int insteadID)
        {
            int ids = 0;
            using (var context = new DataBaseContext())
            {
                int IDs = context.SecretChatGroups
                    .Where(x => x.PartyGUID == cryptoCnvId && x.UserId != insteadID)
                    .Select(x => x.UserId)
                    .First();
                ids = IDs;
            }
            return ids;
        }

        public CryptoConversationModel GetCryptoDialogModel(Guid dialogId)
        {
            using (var context = new DataBaseContext())
            {
                var dialog = context.SecretChat.FirstOrDefault(x => x.PartyGUID == dialogId);
                return new CryptoConversationModel()
                {
                    OpenDate = dialog.Create,
                    CloseDate = dialog.Destroy,
                    RemainingMins = CryptoChatTimesLeft(context, dialogId).MinsLeft,
                    RemainingSecs = CryptoChatTimesLeft(context, dialogId).SecLeft
                };
            }
        }

        private CryptoChatStatus ChatStatus(DataBaseContext context, int userId, int creatorUserId, Guid chatId)
        {
            if (userId == creatorUserId)
            {
                SecretChatGroup chatGroup = context.SecretChatGroups.Where(x => x.UserId != userId && x.PartyGUID == chatId).First();
                if (chatGroup.Accepted)
                    return CryptoChatStatus.Accepted;
                else
                    return CryptoChatStatus.SelfCreated;
            }
            else
            {
                SecretChatGroup chatGroup = context.SecretChatGroups.Where(x => x.UserId == userId && x.PartyGUID == chatId).First();
                if (chatGroup.Accepted)
                    return CryptoChatStatus.Accepted;
                else
                    return CryptoChatStatus.PendingAccepted;
            }
        }
        private FriendModel getChatUser(DataBaseContext context, Guid PartyGUID, ref UsersHandler user, BaseUser userCaller)
        {
            var chatUser = new FriendModel();

            var participators = context.SecretChatGroups.Where(x => x.PartyGUID == PartyGUID).ToList();
            foreach (var participator in participators)
            {
                if (participator.UserId != userCaller.UserId)
                {
                    var userInfo = user.BaseUser(participator.UserId);
                    chatUser.UserId = userInfo.UserId;
                    chatUser.AvatarResizeUri = userInfo.AvatarResizeUri;
                    chatUser.Name = userInfo.Name;
                    chatUser.SurName = userInfo.SurName;
                }
            }
            return chatUser;
        }
        private bool IsCryptoChatExpired(DataBaseContext context, Guid GuidId)
        {
            SecretChat CryptoChat = context.SecretChat.Where(x => x.PartyGUID == GuidId).FirstOrDefault();
            if (CryptoChat != null)
            {
                if (CryptoChat.Destroy < DateTime.Now)
                    return true;
                else
                    return false;
            }
            else return true;
        }
        private CryptoDialogTimeLeft CryptoChatTimesLeft(DataBaseContext context, Guid GuidId)
        {
            SecretChat CryptoChat = context.SecretChat.Where(x => x.PartyGUID == GuidId).FirstOrDefault();
            var timeLeft = new CryptoDialogTimeLeft();
            if (CryptoChat != null)
            {
                bool expired = this.IsCryptoChatExpired(context, GuidId);
                if (!expired)
                {
                    DateTime closedDate = CryptoChat.Destroy;
                    var value = closedDate.Subtract(DateTime.Now);
                    timeLeft.MinsLeft = value.Minutes;
                    timeLeft.SecLeft = value.Seconds;
                }
            }
            return timeLeft;
        }
    }
}