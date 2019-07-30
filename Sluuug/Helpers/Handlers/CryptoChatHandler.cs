using Context;
using Slug.Context;
using Slug.Context.Dto.CryptoConversation;
using Slug.Context.Tables;
using Slug.Hubs;
using Slug.ImageEdit;
using Slug.Model;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
        private readonly int multiple = 5;

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

        public CryptoChatModel GetCryptoChat(string sessionId)
        {
            var model = new CryptoChatModel();
            var userHandler = new UsersHandler();

            model.CurrentChats = new List<CryptoChat>();
            model.AcceptNeeded = new List<CryptoChat>();
            model.SelfCreatedChats = new List<CryptoChat>();

            model.FriendsICanInvite = new List<FriendModel>();

            MyFriendsModel friends = userHandler.GetFriendsBySession(sessionId, 50);
            BaseUser userInformation = userHandler.GetCurrentProfileInfo(sessionId);

            foreach (var item in friends.Friends)
            {
                model.FriendsICanInvite.Add(item);
            }

            using (var context = new DataBaseContext())
            {
                List<SecretChatGroup> fullGroups = context.SecretChatGroups
                    .Where(x => x.UserId == userInformation.UserId)
                    .ToList();

                List<SecretChatGroup> chatGroups = context.SecretChatGroups
                    .Where(x => x.UserId == userInformation.UserId)
                    .ToList();
                chatGroups.ForEach(x => {
                    fullGroups.Add( 
                        context.SecretChatGroups
                        .Where(item => item.PartyGUID == x.PartyGUID && item.UserId != userInformation.UserId)
                        .First() 
                        );
                });


                foreach (SecretChatGroup chatGroup in chatGroups)
                {
                    SecretChat secretChat = context.SecretChat.FirstOrDefault(x => x.PartyGUID == chatGroup.PartyGUID);
                    if (secretChat != null)
                    {
                        CryptoChatStatus status = ChatStatus(context, chatGroup.UserId, secretChat.PartyGUID);

                        var chat = new CryptoChat();
                        chat.GuidId = secretChat.PartyGUID;
                        chat.OpenDate = secretChat.Create;
                        chat.CloseDate = secretChat.Destroy;
                        chat.RemainingMins = chat.CloseDate.Subtract(DateTime.Now).Minutes;
                        chat.RemainingSecs = chat.CloseDate.Subtract(DateTime.Now).Seconds;
                        chat.GuidId = secretChat.PartyGUID;

                        int interlocutorID = fullGroups.First(x => x.PartyGUID == secretChat.PartyGUID &&
                                                              x.UserId != userInformation.UserId)
                                                              .UserId;
                        BaseUser interlocutor = userHandler.GetUserInfo(interlocutorID);
                        chat.InterlocutorName = interlocutor.Name;
                        chat.InterlocutorSurName = interlocutor.SurName;
                        if (status == CryptoChatStatus.SelfCreated)
                            chat.InterlocutorAvatar = Resize.ResizedAvatarUri(interlocutor.AvatarResizeUri, ModTypes.c_scale, 50, 50);
                        else
                            chat.InterlocutorAvatar = Resize.ResizedAvatarUri(interlocutor.AvatarResizeUri, ModTypes.c_scale, 50, 50);

                        //chat.Expired = false;
                        var GuidId = chatGroup.PartyGUID.ToString();
                        var count = context.SecretMessage.Count(x=>x.PartyId == GuidId);
                        if (count > 0)
                        {
                            SecretMessages last = context.SecretMessage.ToList().Last();
                            chat.LastMessage = last.Text;
                            chat.LastMessageSendDate = last.SendingDate;
                            string lastSenderAvatar = userHandler.GetFullUserInfo(last.UserSender).AvatarResizeUri;
                            chat.UserLastMessageSenderAvatar = Resize.ResizedAvatarUri(lastSenderAvatar, ModTypes.c_scale, 50, 50);
                        }


                        if (status == CryptoChatStatus.SelfCreated)
                            model.SelfCreatedChats.Add(chat);
                        else if (status == CryptoChatStatus.Accepted)
                            model.CurrentChats.Add(chat);
                        else if (status == CryptoChatStatus.PendingAccepted)
                            model.AcceptNeeded.Add(chat);
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

        public async Task SaveSecretMessageHashAsync(string ChatId, int UserSenderId, string msgHash)
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

        public CryptoDialogModel GetCryptoDialogs(string session, string GuidId, int page)
        {
            if (page <= 0)
                page = 1;

            var model = new CryptoDialogModel();
            var usersHandler = new UsersHandler();
            int userReaderID = usersHandler.UserIdBySession(session);

            var userInfos = new Dictionary<int, BaseUser>();

            model.GuidId = Guid.Parse(GuidId);
            model.Messages = new List<CryptoMessage>();
            using (var context = new DataBaseContext())
            {
                bool expiredFlag = CryptoChatExpired(context, Guid.Parse(GuidId));
                //if (!expiredFlag)
                //{

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
                        userInfos[item.UserSender] = usersHandler.GetUserInfo(item.UserSender);
                    }
                    bool incommingFlag = false;
                    if (userReaderID != item.UserSender)
                        incommingFlag = true;

                    var CrMessage = new CryptoMessage()
                    {
                        SendDate = item.SendingDate,
                        AvatatURI = Resize.ResizedAvatarUri(userInfos[item.UserSender].AvatarResizeUri, ModTypes.c_scale, 50, 50),
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

        private CryptoChatStatus ChatStatus(DataBaseContext context, int userId, Guid chatId)
        {
                SecretChat secretChat = context.SecretChat.Where(x=> x.PartyGUID == chatId).First();
                if (secretChat.CreatorUserId == userId)
                    return CryptoChatStatus.SelfCreated;

                SecretChatGroup chatGroup = context.SecretChatGroups.Where(x => x.UserId == userId && x.PartyGUID == chatId).First();
                if (chatGroup.Accepted == true)
                    return CryptoChatStatus.Accepted;

                return CryptoChatStatus.PendingAccepted;
        }
        private FriendModel getChatUser(DataBaseContext context, Guid PartyGUID, ref UsersHandler user, BaseUser userCaller)
        {
            var chatUser = new FriendModel();

            var participators = context.SecretChatGroups.Where(x => x.PartyGUID == PartyGUID).ToList();
            foreach (var participator in participators)
            {
                if (participator.UserId != userCaller.UserId)
                {
                    var userInfo = user.GetUserInfo(participator.UserId);
                    chatUser.UserId = userInfo.UserId;
                    chatUser.AvatarResizeUri = userInfo.AvatarResizeUri;
                    chatUser.Name = userInfo.Name;
                    chatUser.SurName = userInfo.SurName;
                }
            }
            return chatUser;
        }
        private bool CryptoChatExpired(DataBaseContext context, Guid GuidId)
        {
            SecretChat CryptoChat = context.SecretChat.Where(x => x.PartyGUID == GuidId).First();
            if (CryptoChat.Destroy > DateTime.Now)
                return false;
            else
                return true;
        }
    }
}