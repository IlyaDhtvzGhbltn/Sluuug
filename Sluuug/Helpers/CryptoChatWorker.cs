using Context;
using Slug.Context;
using Slug.Context.Dto.CryptoConversation;
using Slug.Context.Tables;
using Slug.Hubs;
using Slug.Model;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class CryptoChatWorker
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

        public CreateNewCryptoChatResponse CreateNewCryptoChat(CryptoChatType type, List<Participant> UserIds, int Inviter)
        {
            var newSecretChat = new SecretChat();
            newSecretChat.CreatorUserId = Inviter;
            newSecretChat.Create = DateTime.Now;
            newSecretChat.Destroy = DateTime.Now.Add(CryptoChatDestroyDates[type]);
            newSecretChat.PartyGuid = Guid.NewGuid();
            using (var context = new DataBaseContext())
            {
                context.SecretChat.Add(newSecretChat);
                context.SaveChanges();
                foreach (var userInvited in UserIds)
                {
                    int userId = Convert.ToInt32(userInvited.UserId);
                    var secretGroups = new SecretChatGroup();
                    secretGroups.PartyGUID = newSecretChat.PartyGuid;
                    secretGroups.UserId = userId;
                    context.SecretChatGroup.Add(secretGroups);
                }
                var secretGroup = new SecretChatGroup();
                secretGroup.PartyGUID = newSecretChat.PartyGuid;
                secretGroup.UserId = Inviter;
                secretGroup.Accepted = true;
                context.SecretChatGroup.Add(secretGroup);
                context.SaveChanges();

                var response = new CreateNewCryptoChatResponse();
                response.CryptoGuidId = newSecretChat.PartyGuid;
                response.ExpireDate = newSecretChat.Destroy;

                return response;
            }
        }

        public CryptoChatModel GetCryptoChat(string sessionId)
        {
            var model = new CryptoChatModel();
            var user = new UserWorker();

            model.CurrentChats = new List<CryptoChat>();
            model.AcceptNeeded = new List<CryptoChat>();
            model.SelfCreatedChats = new List<CryptoChat>();

            model.FriendsICanInvite = new List<FriendModel>();

            MyFriendsModel friends = user.GetFriendsBySession(sessionId);
            CutUserInfoModel userInformation = user.GetUserInfo(sessionId);

            foreach (var item in friends.Friends)
            {
                model.FriendsICanInvite.Add(item);
            }

            using (var context = new DataBaseContext())
            {
                var chatIDs = context.SecretChatGroup.Where(x => x.UserId == userInformation.UserId).Select(c => c).ToList();
                foreach (var item in chatIDs)
                {
                    SecretChat secretChat = context.SecretChat.FirstOrDefault(x => x.PartyGuid == item.PartyGUID);
                    if (secretChat != null)
                    {

                        var chat = new CryptoChat();
                        chat.OpenDate = secretChat.Create;
                        chat.GuidId = secretChat.PartyGuid;
                        DateTime destroyChatTime = secretChat.Destroy;
                        if (destroyChatTime < DateTime.Now)
                            chat.ActiveStatus = false;
                        else
                            chat.ActiveStatus = true;
                        chat.Users = new List<FriendModel>();

                        CryptoChatStatus status = ChatStatus(context, item.UserId, secretChat.PartyGuid);
                        chat.GuidId = item.PartyGUID;
                        chat.Users = getChatUser(context, item.PartyGUID, ref user);
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
                var chatGroup = context.SecretChatGroup.Where(x=>x.UserId == userId && x.PartyGUID == chatID).First();
                chatGroup.Accepted = true;
                context.SaveChanges();
            }
        }


        private CryptoChatStatus ChatStatus(DataBaseContext context, int userId, Guid chatId)
        {
                SecretChat secretChat = context.SecretChat.Where(x=> x.PartyGuid == chatId).First();
                if (secretChat.CreatorUserId == userId)
                    return CryptoChatStatus.SelfCreated;

                SecretChatGroup chatGroup = context.SecretChatGroup.Where(x => x.UserId == userId && x.PartyGUID == chatId).First();
                if (chatGroup.Accepted == true)
                    return CryptoChatStatus.Accepted;

                return CryptoChatStatus.PendingAccepted;
        }

        private List<FriendModel> getChatUser(DataBaseContext context, Guid PartyGUID, ref UserWorker user)
        {
            List<FriendModel> chatParticipants = new List<FriendModel>();
            var participators = context.SecretChatGroup.Where(x => x.PartyGUID == PartyGUID).ToList();
            foreach (var participator in participators)
            {
                var friendModel = new FriendModel();
                var userInfo = user.GetUserInfo(participator.UserId);
                friendModel.UserId = userInfo.UserId;
                friendModel.AvatarPath = userInfo.AvatarUri;
                friendModel.Name = userInfo.Name;
                friendModel.SurName = userInfo.SurName;

                chatParticipants.Add(friendModel);
            }
            return chatParticipants;
        }
    }
}