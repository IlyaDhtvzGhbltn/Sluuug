using Context;
using Slug.Context.Dto.UserWorker;
using Slug.Context.Tables;
using Slug.Crypto;
using Slug.Helpers;
using Slug.Model;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Slug.Context
{
    public class UserWorker
    {
        public UserConfirmationDitails RegisterNew(RegisteringUserModel user)
        {
            string activationSessionId = string.Empty;
            string activationMailParam = string.Empty;

            using (var context = new DataBaseContext())
            {
                var newUser = new Context.Tables.User();

                newUser.CountryCode = user.CountryCode;
                newUser.DateOfBirth = user.DateBirth;
                newUser.Email = user.Email;
                newUser.Name = user.Name;
                newUser.ForName = user.ForName;
                newUser.Login = user.Login;
                newUser.Password = Converting.ConvertStringToSHA512(user.PasswordHash);

                newUser.UserStatus = (int)UserStatuses.AwaitConfirmation;

                context.Users.Add(newUser);
                var sesWk = new SessionWorker();
                activationSessionId = sesWk.OpenSession(SessionTypes.AwaitEmailConfirm, 0);
                try
                {
                    context.SaveChanges();
                    var linkMail = new ActivationMailWorker();
                    activationMailParam = linkMail.CreateActivationEntries(context.Users.First(x => x.Email == user.Email).Id);
                    context.SaveChanges();

                }
                catch (DbEntityValidationException e)
                {

                }
            }
            return new UserConfirmationDitails { ActivatioMailParam = activationMailParam, ActivationSessionId = activationSessionId };
        }

        public void ConfirmUser(int id)
        {
            using (var context = new DataBaseContext())
            {
                var user = context.Users.Where(x => x.Id == id).First();
                user.UserStatus = (int)UserStatuses.Active;
                context.SaveChanges();
            }
        }

        public int VerifyUser(string login, string hashPassword)
        {
            string savedPassword = Crypto.Converting.ConvertStringToSHA512(hashPassword);
            using (var dbContext = new DataBaseContext())
            {
                var user = dbContext.Users.Where(x => x.Login == login && x.Password == savedPassword).FirstOrDefault();
                if (user != null)
                    return user.Id;
            }
            return 0;
        }

        public CutUserInfoModel GetUserInfo(string session_id)
        {
            var userModel = new CutUserInfoModel();
            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.First(x => x.Number == session_id);
                var user = context.Users.First(x => x.Id == session.UserId);

                Avatars avatar = context.Avatars.First(x => x.Id == user.AvatarId);
                userModel.Name = user.Name;
                userModel.SurName = user.ForName;
                userModel.Sity = user.Sity;
                userModel.MetroStation = user.MetroStation;
                userModel.DateBirth = user.DateOfBirth;
                userModel.AvatarUri = avatar.ImgPath;
                userModel.UserId = user.Id;
            }
            return userModel;
        }

        public CutUserInfoModel GetUserInfo(int userId)
        {
            var userModel = new CutUserInfoModel();
            using (var context = new DataBaseContext())
            {
                try
                {
                    var user = context.Users.First(x => x.Id == userId);

                    Avatars avatar = context.Avatars.First(x => x.Id == user.AvatarId);
                    userModel.Name = user.Name;
                    userModel.SurName = user.ForName;
                    userModel.Sity = user.Sity;
                    userModel.MetroStation = user.MetroStation;
                    userModel.DateBirth = user.DateOfBirth;
                    userModel.AvatarUri = avatar.ImgPath;
                    userModel.UserId = user.Id;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return userModel;
        }

        public bool CheckConversationBySessionId(string sessionId, Guid conversationGuidId)
        {
            var dW = new DialogWorker();
            var ids = dW.GetConversatorsIds(conversationGuidId);
            CutUserInfoModel user = GetUserInfo(sessionId);
            if (ids != null)
            {
                if (ids.Count() != 0 && ids.Contains(user.UserId))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsUsersAreFriends(string sessionId, int userId)
        {
            var userInfo = GetUserInfo(sessionId);
            using (var context = new DataBaseContext())
            {
                var friendShip = context.FriendsRelationship
                    .Where(x => x.UserOferFrienshipSender == userInfo.UserId || x.UserConfirmer == userInfo.UserId)
                    .Where(x => x.Accepted == true)
                    .ToArray();
                if (friendShip.Count() >= 1)
                {
                    for (int i=0; i < friendShip.Count(); i++)
                    {
                        if (friendShip[i].UserOferFrienshipSender == userId || friendShip[i].UserConfirmer == userId )
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }

        public MyFriendsModel GetFriendsBySession(string sessionId)
        {
            var model = new MyFriendsModel();
            model.Friends = new List<FriendModel>();

            using (var context = new DataBaseContext())
            {
                int userId = GetUserInfo(sessionId).UserId;
                FriendsRelationship[] friendShip = context.FriendsRelationship
                    .Where(x => x.UserOferFrienshipSender == userId || x.UserConfirmer == userId)
                    .Where(x => x.Accepted == true)
                    .ToArray();
                if (friendShip.Count() >= 1)
                {
                    var confirmerIds = friendShip.Where(x => x.UserConfirmer != userId).Select(x => x.UserConfirmer);
                    var acceptedIds = friendShip.Where(x => x.UserOferFrienshipSender != userId).Select(x => x.UserOferFrienshipSender);
                    var FriendsIds = confirmerIds.Concat(acceptedIds).ToArray();

                    for (int i=0; i< FriendsIds.Count(); i++)
                    {
                        CutUserInfoModel friendUserInfo = GetUserInfo(FriendsIds[i]);
                        var friend = new FriendModel()
                        {
                            UserId = friendUserInfo.UserId,
                            AvatarPath = friendUserInfo.AvatarUri,
                            Name = friendUserInfo.Name,
                            SurName = friendUserInfo.SurName
                        };
                        model.Friends.Add(friend);
                    }
                }
            }
            return model;
        }

        public Guid GetConversationId(string userSenderSession, int userRecipientId)
        {
            int userSenderId = GetUserInfo(userSenderSession).UserId;
            Guid guidID = Guid.NewGuid();
            using (var context = new DataBaseContext())
            {
                var ConversationGuids = context.ConversationGroup
                    .Where(user => user.UserId == userSenderId || user.UserId == userRecipientId).ToList();

                if (ConversationGuids.Count == 0)
                {
                    var conv = new ConversationGroup();
                    var conv_ = new ConversationGroup();

                    conv.ConversationGuidId = guidID;
                    conv.UserId = userRecipientId;
                    conv_.ConversationGuidId = guidID;
                    conv_.UserId = userSenderId;

                    context.ConversationGroup.Add(conv);
                    context.ConversationGroup.Add(conv_);

                    var con = new Conversation();
                    con.ConversationGuidId = guidID;
                    con.CreatedDateTime = DateTime.UtcNow;
                    context.Conversations.Add(con);
                    context.SaveChanges();

                    return guidID;
                }

                if (ConversationGuids[0].ConversationGuidId == ConversationGuids[1].ConversationGuidId)
                    return ConversationGuids[0].ConversationGuidId;
            }
            return Guid.Empty;
        }

        public void ChangeAvatarUri(string session, Uri newUri)
        {
            int userID = GetUserInfo(session).UserId;
            string uri = newUri.ToString();
            using (var context = new DataBaseContext())
            {
                var newAvatar = new Avatars();
                newAvatar.UploadTime = DateTime.UtcNow;
                newAvatar.ImgPath = uri;
                context.Avatars.Add(newAvatar);
                context.SaveChanges();

                int avatarSavedID = context.Avatars.First(x=>x.ImgPath == uri).Id;

                User userInfo = context.Users.First(x=>x.Id == userID);
                userInfo.AvatarId = avatarSavedID;

                context.SaveChanges();
            }
        }
    }
}