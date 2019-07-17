using Context;
using Slug.Context;
using Slug.Context.Dto;
using Slug.Context.Dto.Messages;
using Slug.Context.Dto.UserWorker;
using Slug.Context.Tables;
using Slug.Crypto;
using Slug.Helpers;
using Slug.Model;
using Slug.Model.Albums;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;
using NLog;
using Slug.ImageEdit;

namespace Slug.Helpers
{
    public class UsersHandler
    {
        public UserConfirmationDitails RegisterNew(RegisteringUserModel user)
        {
            string activationSessionId = string.Empty;
            string activationMailParam = string.Empty;

            using (var context = new DataBaseContext())
            {
                var loginAlreadyUsed = context.Users.FirstOrDefault(x => x.Login == user.Login);
                var emailAlreadyUsed = context.Users.FirstOrDefault(x => x.Settings.Email == user.Email);
                if (loginAlreadyUsed == null && emailAlreadyUsed == null)
                {

                    if (user.DateBirth >= DateTime.Now)
                    {
                        return null;
                    }

                    var newUser = new User();
                    newUser.Settings = new UserSettings();
                    newUser.UserFullInfo = new UserInfo();
                    newUser.CountryCode = user.CountryCode;
                    newUser.UserFullInfo.DateOfBirth = user.DateBirth;
                    newUser.Settings.Email = user.Email;
                    newUser.UserFullInfo.Name = user.Name;
                    newUser.UserFullInfo.SurName = user.SurName;
                    newUser.Login = user.Login;
                    newUser.Settings.PasswordHash = Converting.ConvertStringToSHA512(user.PasswordHash);
                    newUser.AvatarId = context.Avatars.First(x => x.CountryCode == user.CountryCode).Id;
                    newUser.UserStatus = (int)UserStatuses.AwaitConfirmation;

                    context.Users.Add(newUser);
                    var sesWk = new SessionsHandler();
                    activationSessionId = sesWk.OpenSession(SessionTypes.AwaitEmailConfirm, 0);
                    try
                    {
                        context.SaveChanges();
                        var linkMail = new ActivationHandler();
                        List<User> User = context.Users
                            .Where(x => x.Settings.Email == user.Email).ToList();

                        activationMailParam = linkMail.CreateActivationEntries(User.Last().Id);

                        context.SaveChanges();
                        return new UserConfirmationDitails { ActivatioMailParam = activationMailParam, ActivationSessionId = activationSessionId };

                    }
                    catch (DbEntityValidationException e)
                    {

                    }
                }
            }
            return null;
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
                User user = dbContext.Users
                    .FirstOrDefault(x => x.Settings.PasswordHash == savedPassword && x.Login == login && x.UserStatus == (int)UserStatuses.Active);
                if (user != null)
                    return user.Id;
            }
            return 0;
        }

        public FullUserInfoModel GetFullUserInfo(string sessioID)
        {
            var userModel = new FullUserInfoModel();
            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.First(x => x.Number == sessioID);
                User user = context.Users.First(x => x.Id == session.UserId);

                Avatars avatar = context.Avatars.First(x => x.Id == user.AvatarId);
                userModel.Name = user.UserFullInfo.Name;
                userModel.SurName = user.UserFullInfo.SurName;

                var userCountry = context.Countries
                    .Where(x => x.CountryCode == user.UserFullInfo.NowCountryCode && x.Language == LanguageType.Ru)
                    .FirstOrDefault();

                if (userCountry != null)
                    userModel.Country = userCountry.Title;
                else
                    userModel.Country = "Не указана";

                var City = context.Cities.Where(x => x.CitiesCode == user.UserFullInfo.NowSityCode && x.Language == LanguageType.Ru)
                    .FirstOrDefault();

                if (City != null)
                    userModel.Sity = City.Title;
                else
                    userModel.Sity = "Не указана";

                userModel.DateBirth = user.UserFullInfo.DateOfBirth;
                userModel.AvatarUri = Resize.ResizedUri(avatar.ImgPath, ModTypes.c_scale, 200); //c_scale,h_200,c_thumb,g_face
                userModel.UserId = user.Id;

                userModel.FullAges = new DateTime(DateTime.Now.Subtract(userModel.DateBirth).Ticks).Year;

                var Educations = user.UserFullInfo.Educations;
                userModel.Educations = new List<EducationModel>();
                Educations.ForEach(x=>
                userModel.Educations.Add(new EducationModel()
                {
                     Comment = x.Comment,
                     EducationType = x.EducationType,
                     End = x.End,
                     Faculty = x.Faculty,
                     PersonalRating = x.PersonalRating,
                     Start = x.Start,
                     Specialty = x.Specialty,
                     UntilNow = x.UntilNow,
                     Title = x.Title,
                    EntryId = x.EntryId,


                    Country = context.Countries
                    .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
                    .First().Title,
                     Sity = context.Cities
                    .Where(c => c.CitiesCode == x.SityCode && c.Language == LanguageType.Ru)
                    .First().Title

                }));

                var Events = user.UserFullInfo.Events;
                userModel.Events = new List<MemorableEventsModel>();
                Events.ForEach(x =>
                userModel.Events.Add(new MemorableEventsModel()
                {
                    EventComment = x.EventComment,
                    DateEvent = x.DateEvent,
                    EventTitle = x.EventTitle,
                    EntryId = x.EntryId,

                })
                );

                var Works = user.UserFullInfo.Works;
                userModel.Works = new List<WorkPlacesModel>();
                Works.ForEach(x=>
                userModel.Works.Add(new WorkPlacesModel()
                {
                     Comment = x.Comment,
                     CompanyTitle = x.CompanyTitle,
                     PersonalRating = x.PersonalRating,
                     Position = x.Position,
                     Start = x.Start,
                     End = x.End,
                     UntilNow = x.UntilNow,
                    EntryId = x.EntryId,


                    Country = context.Countries
                    .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
                    .First().Title,
                    Sity = context.Cities
                    .Where(c => c.CitiesCode == x.SityCode && c.Language == LanguageType.Ru)
                    .First().Title

                })
                );

                var Places = user.UserFullInfo.Places;
                userModel.Places = new List<LifePlacesModel>();
                Places.ForEach(x=>
                userModel.Places.Add(new LifePlacesModel()
                {
                    Comment = x.Comment,
                    Start = x.Start,
                    End = x.End,
                    PersonalRating = x.PersonalRating,
                    UntilNow = x.UntilNow,
                    EntryId = x.EntryId,

                    Country = context.Countries
                    .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
                    .First().Title,
                    Sity = context.Cities
                    .Where(c => c.CitiesCode == x.SityCode && c.Language == LanguageType.Ru)
                    .First().Title
                })               
                );

                List<Album> albums = context.Albums.Where(x => x.CreateUserID == user.Id).ToList();
                userModel.Albums = new List<AlbumModel>();
                foreach (var album in albums)
                {
                    AlbumModel albumModel = new AlbumModel
                    {
                        AlbumId = album.Id,
                        AlbumLabelUrl = album.AlbumLabelUrl,
                        AuthorComment = album.Description,
                        CreationTime = album.CreationDate,
                        Title = album.Title,
                        FotosCount = album.Fotos.Count
                    };
                    userModel.Albums.Add(albumModel);
                }                
            }
            return userModel;
        }

        public FullUserInfoModel GetFullUserInfo(int userId)
        {
            var userModel = new FullUserInfoModel();

            using (var context = new DataBaseContext())
            {
                User user = context.Users.First(x => x.Id == userId);
                Avatars avatar = context.Avatars.First(x => x.Id == user.AvatarId);
                userModel.Name = user.UserFullInfo.Name;
                userModel.SurName = user.UserFullInfo.SurName;

                userModel.Country = context.Countries
                    .Where(x => x.CountryCode == user.UserFullInfo.NowCountryCode && x.Language == LanguageType.Ru)
                    .First()
                    .Title;
                userModel.Sity = context.Cities.Where(x => x.CitiesCode == user.UserFullInfo.NowSityCode && x.Language == LanguageType.Ru)
                    .First().Title;

                userModel.DateBirth = user.UserFullInfo.DateOfBirth;
                userModel.AvatarUri = avatar.ImgPath;
                userModel.UserId = user.Id;
                userModel.FullAges = new DateTime(DateTime.Now.Subtract(userModel.DateBirth).Ticks).Year;

                var Educations = user.UserFullInfo.Educations.OrderBy(x=>x.Start).ToList();
                userModel.Educations = new List<EducationModel>();
                Educations.ForEach(x =>
                userModel.Educations.Add(new EducationModel()
                {
                    Comment = x.Comment,
                    EducationType = x.EducationType,
                    End = x.End,
                    Faculty = x.Faculty,
                    PersonalRating = x.PersonalRating,
                    Start = x.Start,
                    Specialty = x.Specialty,
                    UntilNow = x.UntilNow,
                    Title = x.Title,
                    EntryId = x.EntryId,


                    Country = context.Countries
                    .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
                    .First().Title,
                    Sity = context.Cities
                    .Where(c => c.CitiesCode == x.SityCode && c.Language == LanguageType.Ru)
                    .First().Title

                }));
                var Events = user.UserFullInfo.Events.OrderBy(x => x.DateEvent).ToList();
                userModel.Events = new List<MemorableEventsModel>();
                Events.ForEach(x =>
                userModel.Events.Add(new MemorableEventsModel()
                {
                    EventComment = x.EventComment,
                    DateEvent = x.DateEvent,
                    EventTitle = x.EventTitle,
                    EntryId = x.EntryId,

                })
                );

                var Works = user.UserFullInfo.Works.OrderBy(x => x.Start).ToList();
                userModel.Works = new List<WorkPlacesModel>();
                Works.ForEach(x =>
                userModel.Works.Add(new WorkPlacesModel()
                {
                    Comment = x.Comment,
                    CompanyTitle = x.CompanyTitle,
                    PersonalRating = x.PersonalRating,
                    Position = x.Position,
                    Start = x.Start,
                    End = x.End,
                    UntilNow = x.UntilNow,
                    EntryId = x.EntryId,


                    Country = context.Countries
                    .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
                    .First().Title,
                    Sity = context.Cities
                    .Where(c => c.CitiesCode == x.SityCode && c.Language == LanguageType.Ru)
                    .First().Title

                })
                );

                var Places = user.UserFullInfo.Places.OrderBy(x => x.Start).ToList();
                userModel.Places = new List<LifePlacesModel>();
                Places.ForEach(x =>
                userModel.Places.Add(new LifePlacesModel()
                {
                    Comment = x.Comment,
                    Start = x.Start,
                    End = x.End,
                    PersonalRating = x.PersonalRating,
                    UntilNow = x.UntilNow,
                    EntryId = x.EntryId,

                    Country = context.Countries
                    .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
                    .First().Title,
                    Sity = context.Cities
                    .Where(c => c.CitiesCode == x.SityCode && c.Language == LanguageType.Ru)
                    .First().Title
                }));

                List<Album> albums = context.Albums.Where(x => x.CreateUserID == user.Id).ToList();
                userModel.Albums = new List<AlbumModel>();
                foreach (var album in albums)
                {
                    AlbumModel albumModel = new AlbumModel
                    {
                        AlbumId = album.Id,
                        AlbumLabelUrl = album.AlbumLabelUrl,
                        AuthorComment = album.Description,
                        CreationTime = album.CreationDate,
                        Title = album.Title,
                        FotosCount = album.Fotos.Count
                    };
                    userModel.Albums.Add(albumModel);
                }
            }

            return userModel;
        }

        public UserSettings GetUserSettings(string sessionID)
        {
            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.FirstOrDefault(x => x.Number == sessionID);
                if (session != null)
                {
                    User user = context.Users.First(x => x.Id == session.UserId);
                    return user.Settings;
                }
                else return null;
            }
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
                    userModel.Country = context.Countries
                        .Where(x => x.CountryCode == user.UserFullInfo.NowCountryCode && x.Language == LanguageType.Ru)
                        .First()
                        .Title;

                    userModel.Sity = context.Cities
                        .Where(x => x.CitiesCode == user.UserFullInfo.NowSityCode && x.Language == LanguageType.Ru)
                        .First()
                        .Title; 

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
            CutUserInfoModel user = GetFullUserInfo(sessionId);
            if (ids != null)
            {
                if (ids.Count() != 0 && ids.Contains(user.UserId))
                {
                    return true;
                }
            }
            return false;
        }

        public MyFriendsModel GetFriendsBySession(string sessionId, int avatarResize = 100)
        {
            var model = new MyFriendsModel();
            model.Friends = new List<FriendModel>();
            model.IncommingInvitations = new List<FriendModel>();
            model.OutCommingInvitations = new List<FriendModel>();

            using (var context = new DataBaseContext())
            {
                int userId = GetFullUserInfo(sessionId).UserId;
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
                        int friendAges = DateTime.Now.Year - friendUserInfo.DateBirth.Year;
                        
                        var friend = new FriendModel()
                        {
                            UserId = friendUserInfo.UserId,
                            AvatarPath = Resize.ResizedUri(friendUserInfo.AvatarUri, ModTypes.c_scale, avatarResize),
                            Name = friendUserInfo.Name,
                            SurName = friendUserInfo.SurName,
                            Country = friendUserInfo.Country,
                            City = friendUserInfo.Sity,
                            Age = friendAges
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
                            AvatarPath = Resize.ResizedUri(friendUserInfo.AvatarUri, ModTypes.c_scale, 100),
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
                            AvatarPath = Resize.ResizedUri(friendUserInfo.AvatarUri, ModTypes.c_scale, 100),
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
            int userSenderId = GetFullUserInfo(userSenderSession).UserId;
            Guid guidID = Guid.NewGuid();
            using (var context = new DataBaseContext())
            {
                List<Guid> ConversationSenderGuids = context.ConversationGroup
                    .Where(user => user.UserId == userSenderId)
                    .Select(x=>x.ConversationGuidId)
                    .ToList();

                List<Guid> ConversationRecipientGuids = context.ConversationGroup
                    .Where(user => user.UserId == userRecipientId)
                    .Select(x => x.ConversationGuidId)
                    .ToList();

                var intersectGuids = ConversationSenderGuids.Intersect(ConversationRecipientGuids).ToList();

                if (intersectGuids.Count == 0)
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
                else
                {
                    return intersectGuids[0];
                }
            }
            return Guid.Empty;
        }

        public void ChangeAvatarUri(string session, Uri newUri)
        {
            int userID = GetFullUserInfo(session).UserId;
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
            var model = new UserSettingsModel();
            CutUserInfoModel user = GetFullUserInfo(session);
            using (var context = new DataBaseContext())
            {
                User userSett = context.Users.Where(x => x.Id == user.UserId).First();
                model.NotifyType = userSett.Settings.NotificationType;
                model.Email = userSett.Settings.Email;
                model.QuickMessage = userSett.Settings.QuickMessage;
            }
            return model;
        }

        public CutUserInfoModel AddInviteToContacts(string session, int userIDToFriendsInvite)
        {
            CutUserInfoModel userSenderRequest = GetFullUserInfo(session);
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
                var userInfo = GetUserInfo(userID);
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
            int myID = GetFullUserInfo(session).UserId;
            bool isUsersFriends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(session, userID);
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

        public async Task<NotifyHubModel> AcceptInviteToContacts(string session, int userID)
        {
            CutUserInfoModel accepterUser = GetFullUserInfo(session);
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
                var connections = connectionHandler.GetConnectionById(userID);
                var responce = new NotifyHubModel();
                responce.ConnectionIds = connections.ConnectionId;
                responce.FromUser = accepterUser;
                responce.Culture = connections.CultureCode[0];
                return responce;
            }
        }

        public ChangeParameterResponce ChangeParameter(string session, UserParams parameter, string newValue)
        {
            MatchCollection matches = SlugController.ValidateSymbols.Matches(newValue);
            if (matches.Count > 0)
            {
                return new ChangeParameterResponce
                {
                    IsSuccess = false,
                    Message = ChangeParameterResponce.Errors.INVALID_CHARACTERS
                };
            }
            else
            {
                using (var context = new DataBaseContext())
                {
                    FullUserInfoModel user = GetFullUserInfo(session);
                    
                    if (user == null)
                    {
                        return new ChangeParameterResponce
                        {
                            IsSuccess = false,
                            Message = ChangeParameterResponce.Errors.ACCESS_DENIED
                        };
                    }
                    else
                    {
                        User s_user = context.Users.First(x => x.Id == user.UserId);
                        switch (parameter)
                        {
                            case UserParams.UserName :
                                s_user.UserFullInfo.Name = newValue;
                                break;
                            case UserParams.UserSurname:
                                s_user.UserFullInfo.SurName = newValue;
                                break;
                        }
                        context.SaveChanges();
                        return new ChangeParameterResponce()
                        {
                            IsSuccess = true,
                            Message = parameter.ToString()
                        };
                    }
                }
            }
        }

        public int IsEmailValid(string email)
        {
            try
            {
                MailAddress addres = new MailAddress(email);
                using (var context = new DataBaseContext())
                {
                    var user = context.Users.FirstOrDefault(x=>x.Settings.Email == email);
                    if (user != null)
                        return user.Id;
                }
            }
            catch (Exception ex)
            {
                Logger loggerInternal = LogManager.GetLogger("internal_error_logger");
                loggerInternal.Error(ex);
            }
            return 0;
        }
    }
}