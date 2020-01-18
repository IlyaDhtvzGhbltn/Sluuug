using Context;
using Slug.Context;
using Slug.Context.Dto.Conversation;
using Slug.Context.Dto.CryptoConversation;
using Slug.Context.Tables;
using Slug.Helpers.BaseController;
using Slug.Helpers.Handlers.HandlersInterface;
using Slug.Hubs;
using Slug.ImageEdit;
using Slug.Model;
using Slug.Model.Messager.CryptoChat;
using Slug.Model.Users;
using Slug.Model.Users.Relations;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var vipUsers = new VipUsersHandler();
            int userSenderID = userHandler.UserIdBySession(sessionId);

            var model = new CryptoConversationGroupModel();
            model.CurrentActiveChats = new List<CryptoConversationModel>();
            model.IncommingInviters = new List<CryptoConversationModel>();
            model.OutCommingInviters = new List<CryptoConversationModel>();
            model.FriendsICanInvite = new List<CryptoDialogUser>();


            model.FriendsICanInvite = userHandler.GetAvaliableToCryptoDialog(userSenderID).GetAwaiter().GetResult();

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
                        CryptoChatStatus status = chatStatus(context, chatGroup.UserId, cryptoChatCreatorUserId, secretChat.PartyGUID);

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
                        if (interlocutor != null)
                        {
                            chat.InterlocutorName = interlocutor.Name;
                            chat.InterlocutorSurName = interlocutor.SurName;
                            chat.InterlocutorAvatar = interlocutor.LargeAvatar;
                            chat.InterlocutorVIP = interlocutor.Vip;

                            var GuidId = chatGroup.PartyGUID;
                            var count = context.SecretMessage.Count(x => x.PartyId == GuidId);
                            if (count > 0)
                            {
                                SecretMessages last = context.SecretMessage.ToList().Last();
                                BaseUser lastMessageSenderInfo = userHandler.BaseUser(last.UserSender);

                                chat.LastMessage = last.Text;
                                chat.LastMessageSendDate = last.SendingDate.ToString("HH:mm", new CultureInfo("ru-RU"));
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
                    RemainingMins = cryptoChatTimesLeft(context, dialogId).MinsLeft,
                    RemainingSecs = cryptoChatTimesLeft(context, dialogId).SecLeft
                };
            }
        }
        public async Task<CryptoDialogModel> GetCryptoDialogs(string session, Guid guidId, int page)
        {
            if (page <= 0)
                page = 1;

            var model = new CryptoDialogModel();
            var usersHandler = new UsersHandler();
            int userReaderID = usersHandler.UserIdBySession(session);

            var userInfos = new Dictionary<int, BaseUser>();

            model.GuidId = guidId;
            model.Messages = new List<CryptoMessageModel>();
            using (var context = new DataBaseContext())
            {
                if (userPermittedDialog(context, guidId, userReaderID))
                {
                    await readUnreadMsg(context, guidId, userReaderID);
                    var query = context.SecretMessage.Where(x => x.PartyId == guidId);
                    int totalMessageCount = query.Count();

                    if (!isCryptoChatExpired(context, guidId))
                    {
                        var leftTime = cryptoChatTimesLeft(context, guidId);
                        model.MinsLeft = leftTime.MinsLeft;
                        model.SecLeft = leftTime.SecLeft;
                        model.NotExpired = true;
                    }
                    model.TotalDialogMessagesCount = totalMessageCount;

                    int skipCount = totalMessageCount > multiple ? totalMessageCount - multiple : 0;

                    List<SecretMessages> cryptoMessageCollection = query
                        .OrderBy(x => x.Id)
                        .Skip(skipCount)
                        .Take(multiple)
                        .ToList();

                    foreach (var item in cryptoMessageCollection)
                    {
                        bool incommingFlag = false;

                        if (!userInfos.ContainsKey(item.UserSender))
                            userInfos[item.UserSender] = usersHandler.BaseUser(item.UserSender);
                        if (userReaderID != item.UserSender)
                            incommingFlag = true;

                        var CrMessage = new CryptoMessageModel()
                        {
                            SendingDate = item.SendingDate.ToString("HH:mm", new CultureInfo("ru-RU")),
                            AvatatURI = userInfos[item.UserSender].SmallAvatar,
                            Text = item.Text,
                            Name = userInfos[item.UserSender].Name,
                            SurName = userInfos[item.UserSender].SurName,
                            IsIncomming = incommingFlag,
                            UserSenderId = item.UserSender
                        };
                        model.Messages.Add(CrMessage);
                    }
                    return model;
                }
                return null;
            }
        }
        public MoreCryptoDialogMessagesResponce GetMoreMessages(MoreMessagesDialogRequest request)
        {
            using (var context = new DataBaseContext())
            {
                if (userPermittedDialog(context, request.DialogId, request.UserId))
                {
                    var userInfos = new Dictionary<int, BaseUser>();
                    var query = context.SecretMessage.Where(x => x.PartyId == request.DialogId);
                    var messages = query
                        .OrderByDescending(x => x.SendingDate)
                        .Skip(request.LoadedMessages)
                        .Take(multiple)
                        .ToList();

                    var resp = new MoreCryptoDialogMessagesResponce()
                    {
                        DialogTotalMessageCount = query.Count()
                    };

                    resp.Messages = new List<CryptoMessageModel>();
                    var usersHandler = new UsersHandler();

                    messages.ForEach(msg => 
                    {
                        if (!userInfos.ContainsKey(msg.UserSender))
                            userInfos[msg.UserSender] = usersHandler.BaseUser(msg.UserSender, context);

                        resp.Messages.Add(new CryptoMessageModel()
                        {
                            DialogId = msg.PartyId,
                            Text = msg.Text,
                            SendingDate = msg.SendingDate.ToString("f", new CultureInfo("ru-RU")),
                            UserSenderId = msg.UserSender,

                            AvatatURI = userInfos[msg.UserSender].SmallAvatar,
                            Name = userInfos[msg.UserSender].Name,
                            SurName = userInfos[msg.UserSender].SurName,
                            IsIncomming = msg.UserSender == request.UserId ? false : true
                        });
                    });
                    return resp;
                }
                return null;
            }
        }


        private async Task readUnreadMsg(DataBaseContext context, Guid guidId,  int userReaderID)
        {
            var notReadMessage = context.SecretMessage
                .Where(x =>
                x.PartyId == guidId &&
                x.UserSender != userReaderID)
                .ToList();

            notReadMessage.ForEach(x =>
                x.IsReaded = true);
            await context.SaveChangesAsync();
        }
        private bool userPermittedDialog(DataBaseContext context, Guid cryptoDialogId, int userId)
        {
            SecretChatGroup item = context.SecretChatGroups
                .Where(x => x.PartyGUID == cryptoDialogId && x.UserId == userId)
                .FirstOrDefault();
            if (item != null)
                return true;
            else return false;
        }
        private CryptoChatStatus chatStatus(DataBaseContext context, int userId, int creatorUserId, Guid chatId)
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
                    chatUser.LargeAvatar = userInfo.LargeAvatar;
                    chatUser.Name = userInfo.Name;
                    chatUser.SurName = userInfo.SurName;
                }
            }
            return chatUser;
        }
        private bool isCryptoChatExpired(DataBaseContext context, Guid GuidId)
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
        private CryptoDialogTimeLeft cryptoChatTimesLeft(DataBaseContext context, Guid GuidId)
        {
            SecretChat CryptoChat = context.SecretChat.Where(x => x.PartyGUID == GuidId).FirstOrDefault();
            var timeLeft = new CryptoDialogTimeLeft();
            if (CryptoChat != null)
            {
                bool expired = this.isCryptoChatExpired(context, GuidId);
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