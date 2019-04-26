using Context;
using Slug.Context.Dto.Messages;
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
using System.Threading.Tasks;

namespace Slug.Context
{
    public class UsersHandler
    {
        public UserConfirmationDitails RegisterNew(RegisteringUserModel user)
        {
            string activationSessionId = string.Empty;
            string activationMailParam = string.Empty;

            using (var context = new DataBaseContext())
            {
                var newUser = new Context.Tables.User();

                newUser.CountryCode = user.CountryCode;
                newUser.UserFullInfo.DateOfBirth = user.DateBirth;
                newUser.Settings.Email = user.Email;
                newUser.UserFullInfo.Name = user.Name;
                newUser.UserFullInfo.SurName = user.ForName;
                newUser.Login = user.Login;
                newUser.Settings.PasswordHash = Converting.ConvertStringToSHA512(user.PasswordHash);

                newUser.UserStatus = (int)UserStatuses.AwaitConfirmation;

                context.Users.Add(newUser);
                var sesWk = new SessionsHandler();
                activationSessionId = sesWk.OpenSession(SessionTypes.AwaitEmailConfirm, 0);
                try
                {
                    context.SaveChanges();
                    var linkMail = new ActivationHandler();
                    activationMailParam = linkMail.CreateActivationEntries(context.Users.First(x => x.Settings.Email == user.Email).Id);
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
                var user = dbContext.Users.Where(x => x.Login == login && x.Settings.PasswordHash == savedPassword).FirstOrDefault();
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
                userModel.Name = user.UserFullInfo.Name;
                userModel.SurName = user.UserFullInfo.SurName;
                userModel.Sity = user.UserFullInfo.NowSity;
                userModel.MetroStation = user.UserFullInfo.NowMetroStation;
                userModel.DateBirth = user.UserFullInfo.DateOfBirth;
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
                    userModel.Name = user.UserFullInfo.Name;
                    userModel.SurName = user.UserFullInfo.SurName;
                    userModel.Sity = user.UserFullInfo.NowSity;
                    userModel.MetroStation = user.UserFullInfo.NowMetroStation;
                    userModel.DateBirth = user.UserFullInfo.DateOfBirth;
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
            var dW = new UsersDialogHandler();
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
                    .Where(x => x.Status == FriendshipItemStatus.Accept)
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
            model.IncommingInvitations = new List<FriendModel>();
            model.OutCommingInvitations = new List<FriendModel>();

            using (var context = new DataBaseContext())
            {
                int userId = GetUserInfo(sessionId).UserId;
                FriendsRelationship[] friendshipAccepted = context.FriendsRelationship
                    .Where(x => x.UserOferFrienshipSender == userId || x.UserConfirmer == userId)
                    .Where(x => x.Status == FriendshipItemStatus.Accept)
                    .ToArray();
                FriendsRelationship[] inCommingFriendshipPending = context.FriendsRelationship
                    .Where(x => x.UserConfirmer == userId)
                    .Where(x => x.Status == FriendshipItemStatus.Pending)
                    .ToArray();

                FriendsRelationship[] outCommingFriendshipPending = context.FriendsRelationship
                .Where(x => x.UserOferFrienshipSender == userId)
                .Where(x => x.Status == FriendshipItemStatus.Pending)
                .ToArray();

                if (friendshipAccepted.Count() >= 1)
                {
                    var confirmerIds = friendshipAccepted.Where(x => x.UserConfirmer != userId).Select(x => x.UserConfirmer);
                    var acceptedIds = friendshipAccepted.Where(x => x.UserOferFrienshipSender != userId).Select(x => x.UserOferFrienshipSender);

                    var FriendsConfirmIDs = confirmerIds.Concat(acceptedIds).ToArray();

                    for (int i=0; i< FriendsConfirmIDs.Count(); i++)
                    {
                        CutUserInfoModel friendUserInfo = GetUserInfo(FriendsConfirmIDs[i]);
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

                if (inCommingFriendshipPending.Count() >= 1)
                {

                    for (int i = 0; i < inCommingFriendshipPending.Count(); i++)
                    {
                        CutUserInfoModel friendUserInfo = GetUserInfo(inCommingFriendshipPending[i].UserOferFrienshipSender);
                        var inInvite = new FriendModel()
                        {
                            UserId = friendUserInfo.UserId,
                            AvatarPath = friendUserInfo.AvatarUri,
                            Name = friendUserInfo.Name,
                            SurName = friendUserInfo.SurName
                        };
                        model.IncommingInvitations.Add(inInvite);
                    }
                }

                if (outCommingFriendshipPending.Count() >= 1)
                {
                    for (int i = 0; i < outCommingFriendshipPending.Count(); i++)
                    {
                        CutUserInfoModel friendUserInfo = GetUserInfo(outCommingFriendshipPending[i].UserConfirmer);
                        var outInvite = new FriendModel()
                        {
                            UserId = friendUserInfo.UserId,
                            AvatarPath = friendUserInfo.AvatarUri,
                            Name = friendUserInfo.Name,
                            SurName = friendUserInfo.SurName
                        };
                        model.OutCommingInvitations.Add(outInvite);
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

        public UserSettingsModel GetSettings(string session)
        {
            return new UserSettingsModel();
        }

        public CutUserInfoModel AddInviteToContacts(string session, int userIDToFriendsInvite)
        {
            CutUserInfoModel userSenderRequest = GetUserInfo(session);
            using (var context = new DataBaseContext())
            {
                User invitedUser = context.Users.FirstOrDefault(x => x.Id == userIDToFriendsInvite);
                if (invitedUser != null)
                {
                    FriendsRelationship invitationAlreadySand = context.FriendsRelationship
                        .FirstOrDefault(x => x.UserOferFrienshipSender == userSenderRequest.UserId && x.UserConfirmer == invitedUser.Id ||
                        x.UserConfirmer == userSenderRequest.UserId && x.UserOferFrienshipSender == invitedUser.Id);
                    if (invitationAlreadySand == null)
                    {
                        var relation = new FriendsRelationship();
                        relation.OfferSendedDate = DateTime.UtcNow;
                        relation.UserOferFrienshipSender = userSenderRequest.UserId;
                        relation.UserConfirmer = invitedUser.Id;
                        relation.Status = FriendshipItemStatus.Pending;
                        context.FriendsRelationship.Add(relation);
                        context.SaveChanges();
                        return userSenderRequest;
                    }
                    else if (invitationAlreadySand.Status == FriendshipItemStatus.Close)
                    {
                        invitationAlreadySand.Status = FriendshipItemStatus.Pending;
                        context.SaveChanges();
                        return userSenderRequest;
                    }
                }
            }
            return null;
        }

        public ForeignUserViewModel GetForeignUserInfo(string session, int userID)
        {
            var model = new ForeignUserViewModel();
            using (var context = new DataBaseContext())
            {
                var userInfo = GetUserInfo(session);
                model.AvatarPath = userInfo.AvatarUri;
                model.Name = userInfo.Name;
                model.SurName = userInfo.SurName;
                FriendsRelationship relationItem = context.FriendsRelationship
                    .Where(x => x.UserConfirmer == userID && x.UserOferFrienshipSender == userInfo.UserId ||
                    x.UserOferFrienshipSender == userID && x.UserConfirmer == userInfo.UserId )
                    .FirstOrDefault();
                model.Status = FriendshipItemStatus.None;

                if (relationItem != null)
                {
                    model.Status = relationItem.Status;
                }
            }
            return model;
        }

        public void DropFrienship(string session, int userID)
        {
            int myID = GetUserInfo(session).UserId;
            bool isUsersFriends = IsUsersAreFriends(session, userID);
            if (isUsersFriends)
            {
                using (var context = new DataBaseContext())
                {
                    FriendsRelationship entryFrienship = context.FriendsRelationship
                        .Where(x => x.UserOferFrienshipSender == myID && x.UserConfirmer == userID ||
                        x.UserConfirmer == myID && x.UserOferFrienshipSender == userID)
                        .First();
                    entryFrienship.Status = FriendshipItemStatus.Close;
                    context.SaveChanges();
                }
            }
        }

        public async Task<PartialHubResponse> AcceptInviteToContacts(string session, int userID)
        {
            CutUserInfoModel accepterUser = GetUserInfo(session);
            using (var context = new DataBaseContext())
            {
                FriendsRelationship item = context.FriendsRelationship
                    .Where(x => x.Status == FriendshipItemStatus.Pending && 
                    x.UserOferFrienshipSender == userID && 
                    x.UserConfirmer == accepterUser.UserId)
                    .First();

                item.Status = FriendshipItemStatus.Accept;
                context.SaveChanges();

                var connectionHandler = new UsersConnectionHandler();
                IList<string> connections = connectionHandler.GetConnectionById(userID);
                var responce = new PartialHubResponse();
                responce.ConnectionIds = connections;
                responce.FromUser = accepterUser;
                return responce;
            }
        }
    }
}