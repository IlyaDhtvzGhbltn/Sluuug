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
using Slug.Model.Users.Relations;
using Slug.Context.Dto.Search;
using System.Globalization;

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
                    newUser.UserFullInfo.NowCountryCode = user.CountryCode;
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

        public ProfileModel ProfileInfo(string sessioID, bool resize = true)
        {
            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.First(x => x.Number == sessioID);
                int userId = session.UserId;
                return profileInfo(userId, context, resize);
            }
        }

        public ProfileModel ProfileInfo(int userId, bool resize = true)
        {
            var model = new ProfileModel();
            using (var context = new DataBaseContext())
            {
                return profileInfo(userId, context, resize);
            }
        }

        private ProfileModel profileInfo(int userId, DataBaseContext context,  bool resize = true)
        {
            var userModel = new ProfileModel();
            User user = context.Users.First(x => x.Id == userId);
            Avatars avatar = context.Avatars.First(x => x.Id == user.AvatarId);
            userModel.Name = user.UserFullInfo.Name;
            userModel.SurName = user.UserFullInfo.SurName;
            userModel.HelloMessage = user.UserFullInfo.HelloMessage;
            userModel.purpose = (DatingPurposeEnum)user.UserFullInfo.DatingPurpose;
            userModel.userSearchSex = (SexEnum)user.UserFullInfo.userDatingSex;
            userModel.userSearchAge = (AgeEnum)user.UserFullInfo.userDatingAge;

            var userCountry = context.Countries
                .Where(x => x.CountryCode == user.UserFullInfo.NowCountryCode && x.Language == LanguageType.Ru)
                .FirstOrDefault();

            if (userCountry != null)
                userModel.Country = userCountry.Title;
            else
                userModel.Country = "Не указана";

            var City = context.Cities.Where(x => x.CitiesCode == user.UserFullInfo.NowCityCode && x.Language == LanguageType.Ru)
                .FirstOrDefault();

            if (City != null)
                userModel.City = City.Title;
            else
                userModel.City = "Не указанo";
            if (resize)
                userModel.AvatarResizeUri = Resize.ResizedAvatarUri(avatar.ImgPath, ModTypes.c_scale, 200, 200); //c_scale,h_200,c_thumb,g_face
            else
                userModel.AvatarResizeUri = avatar.ImgPath;

            userModel.UserId = user.Id;

            userModel.Age = new DateTime(DateTime.Now.Subtract(user.UserFullInfo.DateOfBirth).Ticks).Year;

            var Educations = user.UserFullInfo.Educations;
            userModel.Educations = new List<EducationModel>();
            Educations.ForEach(x =>
            {
                string endDate = (x.End == null) ? endDate = "настоящее время" : endDate = ((DateTime)x.End).ToString("D");
                userModel.Educations.Add(new EducationModel()
                {
                    Comment = x.Comment,
                    EducationType = x.EducationType,
                    //Faculty = x.Faculty,
                    StartDateFormat = x.Start.ToString("D", CultureInfo.CreateSpecificCulture("ru-RU")),
                    EndDateFormat = endDate,

                    Specialty = x.Specialty,
                    UntilNow = x.UntilNow,
                    Title = x.Title,
                    Id = x.Id,

                    Country = context.Countries
                    .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
                    .First().Title,

                    City = context.Cities
                    .Where(c => c.CitiesCode == x.CityCode && c.Language == LanguageType.Ru)
                    .First().Title

                });
            });

            var Events = user.UserFullInfo.Events;
            userModel.Events = new List<MemorableEventsModel>();
            Events.ForEach(x =>
            {
                string endDate = (x.DateEvent == null) ? endDate = "настоящее время" : endDate = ((DateTime)x.DateEvent).ToString("D");
                var eventModel = new MemorableEventsModel()
                {
                    Text = x.TextEventDescription,
                    DateEventFormat = endDate,
                    EventTitle = x.EventTitle,
                    Id = x.Id,
                    Photos = new List<FotoModel>()
                };
                List<Foto> eventPhotos = context.Fotos.Where(y => y.AlbumID == x.AlbumGuid).ToList();
                if (eventPhotos != null && eventPhotos.Count > 0)
                {
                    eventPhotos.ForEach(f =>
                    {
                        var commentsModel = new List<FotoCommentModel>();
                        var comments = context.FotoComments.Where(c => c.Foto.FotoGUID == f.FotoGUID).ToList();
                        comments.ForEach(fm =>
                        {
                            var userCommenter = context.Users.First(u => u.Id == fm.UserCommenter);
                            Avatars ava = context.Avatars.First(a => a.Id == userCommenter.AvatarId);

                            commentsModel.Add(new FotoCommentModel()
                            {
                                UserName = userCommenter.UserFullInfo.Name,
                                UserSurName = userCommenter.UserFullInfo.SurName,
                                UserPostedID = userCommenter.Id,
                                UserPostedAvatarResizeUri = Resize.ResizedAvatarUri(ava.ImgPath, ModTypes.c_scale, 50, 50),
                                DateFormat = fm.CommentWriteDate.ToString("D"),
                                Text = fm.CommentText
                            });
                        });

                        eventModel.Photos.Add(new FotoModel()
                        {
                            Album = x.AlbumGuid,
                            ID = f.FotoGUID,
                            UploadDate = f.UploadDate,
                            SmallFotoUri = Resize.ResizedFullPhoto(f.Url, f.Height, f.Width, 80, 80),
                            FullFotoUri = Resize.ResizedFullPhoto(f.Url, f.Height, f.Width),
                            DownloadFotoUri = f.Url,
                            FotoComments = commentsModel
                        });
                    });
                }

                userModel.Events.Add(eventModel);
            });

            var Works = user.UserFullInfo.Works;
            userModel.Works = new List<WorkPlacesModel>();
            Works.ForEach(x =>
            {
                string endDate = (x.Start == null) ? endDate = "настоящее время" : endDate = ((DateTime)x.Start).ToString("D");

                userModel.Works.Add(new WorkPlacesModel()
                {
                    Comment = x.Comment,
                    CompanyTitle = x.CompanyTitle,
                    Position = x.Position,
                    StartDateFormat = x.Start.ToString("D"),
                    EndDateFormat = endDate,
                    UntilNow = x.UntilNow,
                    Id = x.Id,

                    Country = context.Countries
                    .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
                    .First().Title,

                    City = context.Cities
                    .Where(c => c.CitiesCode == x.CityCode && c.Language == LanguageType.Ru)
                    .First().Title
                });
            });

            List<Album> albums = context.Albums.Where(x => x.CreateUserID == user.Id && !x.Title.Contains("_event")).ToList();
            userModel.Albums = new List<AlbumModel>();
            foreach (var album in albums)
            {
                AlbumModel albumModel = new AlbumModel
                {
                    AlbumId = album.Id,
                    AlbumLabelUrl = Resize.ResizedFullPhoto(album.AlbumLabelUrl, album.LabelOriginalHeight, album.LabelOriginalWidth, 90, 90),
                    AlbumDescription = album.Description,
                    CreationTime = album.CreationDate,
                    AlbumTitle = album.Title,
                    FotosCount = album.Fotos.Count
                };
                userModel.Albums.Add(albumModel);
            }
            
        return userModel;
        }

        //public FriendModel GetFullUserInfo(int userId)
        //{
        //    var userModel = new FriendModel();

        //    using (var context = new DataBaseContext())
        //    {
        //        User user = context.Users.First(x => x.Id == userId);
        //        Avatars avatar = context.Avatars.First(x => x.Id == user.AvatarId);
        //        userModel.Name = user.UserFullInfo.Name;
        //        userModel.SurName = user.UserFullInfo.SurName;

        //        userModel.Country = context.Countries
        //            .Where(x => x.CountryCode == user.UserFullInfo.NowCountryCode && x.Language == LanguageType.Ru)
        //            .First()
        //            .Title;
        //        userModel.City = context.Cities.Where(x => x.CitiesCode == user.UserFullInfo.NowCityCode && x.Language == LanguageType.Ru)
        //            .First().Title;

        //        userModel.AvatarResizeUri = Resize.ResizedAvatarUri(avatar.ImgPath, ModTypes.c_scale, 200, 200);
        //        userModel.UserId = user.Id;
        //        userModel.Age = new DateTime(DateTime.Now.Subtract(user.UserFullInfo.DateOfBirth).Ticks).Year;

        //        List<Album> albums = context.Albums.Where(x => x.CreateUserID == user.Id).ToList();
        //        userModel.Albums = new List<AlbumModel>();
        //        foreach (var album in albums)
        //        {
        //            AlbumModel albumModel = new AlbumModel
        //            {
        //                AlbumId = album.Id,
        //                AlbumLabelUrl = album.AlbumLabelUrl,
        //                AlbumDescription = album.Description,
        //                CreationTime = album.CreationDate,
        //                AlbumTitle = album.Title,
        //                FotosCount = album.Fotos.Count
        //            };
        //            userModel.Albums.Add(albumModel);
        //        }


        //        var Educations = user.UserFullInfo.Educations.OrderBy(x => x.Start).ToList();

        //        userModel.Educations = new List<EducationModel>();
        //        Educations.ForEach(x =>
        //        userModel.Educations.Add(new EducationModel()
        //        {
        //            Comment = x.Comment,
        //            EducationType = x.EducationType,
        //            End = x.End,
        //            //Faculty = x.Faculty,
        //            Start = x.Start,
        //            Specialty = x.Specialty,
        //            UntilNow = x.UntilNow,
        //            Title = x.Title,
        //            Id = x.Id,


        //            Country = context.Countries
        //            .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
        //            .First().Title,

        //            City = context.Cities
        //            .Where(c => c.CitiesCode == x.CityCode && c.Language == LanguageType.Ru)
        //            .First().Title

        //        }));


        //        var Works = user.UserFullInfo.Works.OrderBy(x => x.Start).ToList();
        //        userModel.Works = new List<WorkPlacesModel>();
        //        Works.ForEach(x =>
        //        userModel.Works.Add(new WorkPlacesModel()
        //        {
        //            Comment = x.Comment,
        //            CompanyTitle = x.CompanyTitle,
        //            Position = x.Position,
        //            Start = x.Start,
        //            End = x.End,
        //            UntilNow = x.UntilNow,
        //            Id = x.Id,

        //            Country = context.Countries
        //            .Where(c => c.CountryCode == x.CountryCode && c.Language == LanguageType.Ru)
        //            .First().Title,

        //            City = context.Cities
        //            .Where(c => c.CitiesCode == x.CityCode && c.Language == LanguageType.Ru)
        //            .First().Title

        //        })
        //        );

        //        var Events = user.UserFullInfo.Events.OrderBy(x => x.DateEvent).ToList();
        //        userModel.Events = new List<MemorableEventsModel>();
        //        Events.ForEach(x => {
        //            string endDate = x.DateEvent.ToString("D");

        //            userModel.Events.Add(new MemorableEventsModel()
        //            {
        //                Text = x.TextEventDescription,
        //                DateEventFormat = endDate,
        //                EventTitle = x.EventTitle,
        //                Id = x.Id,

        //            });
        //        }
        //        );


        //    }

        //    return userModel;
        //}

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

        public BaseUser BaseUser(int userId)
        {
            var userModel = new BaseUser();
            using (var context = new DataBaseContext())
            {
                try
                {
                    var user = context.Users.FirstOrDefault(x => x.Id == userId);
                    if (user != null)
                    {
                        Avatars avatar = context.Avatars.First(x => x.Id == user.AvatarId);
                        userModel.Name = user.UserFullInfo.Name;
                        userModel.SurName = user.UserFullInfo.SurName;
                        userModel.Country = context.Countries
                            .Where(x => x.CountryCode == user.UserFullInfo.NowCountryCode && x.Language == LanguageType.Ru)
                            .First()
                            .Title;

                        userModel.City = context.Cities
                            .Where(x => x.CitiesCode == user.UserFullInfo.NowCityCode && x.Language == LanguageType.Ru)
                            .First()
                            .Title;

                        userModel.AvatarResizeUri = avatar.ImgPath;
                        userModel.UserId = user.Id;
                        userModel.Age = new DateTime(DateTime.Now.Subtract(user.UserFullInfo.DateOfBirth).Ticks).Year;
                        userModel.HelloMessage = user.UserFullInfo.HelloMessage;
                        userModel.userSearchAge = (AgeEnum)user.UserFullInfo.userDatingAge;
                        userModel.userSearchSex = (SexEnum)user.UserFullInfo.userDatingSex;
                        userModel.purpose = (DatingPurposeEnum)user.UserFullInfo.DatingPurpose;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {

                }
            }
            return userModel;
        }

        public bool CheckConversationBySessionId(string sessionId, Guid conversationGuidId)
        {
            var dW = new UsersDialogHandler();
            var ids = dW.GetConversatorsIds(conversationGuidId);
            BaseUser user = ProfileInfo(sessionId);
            if (ids != null)
            {
                if (ids.Count() != 0 && ids.Contains(user.UserId))
                {
                    return true;
                }
            }
            return false;
        }

        public List<BaseUser> GetFriendsOnlyBySession(string sessionId, int friendAvatarResize = 80)
        {
            int userId = UserIdBySession(sessionId);
            var model = new List<BaseUser>();

            using (var context = new DataBaseContext())
            {
                FriendsRelationship[] friendshipAccepted = context.FriendsRelationship
                    .Where(x => x.UserOferFrienshipSender == userId || x.UserConfirmer == userId)
                    .Where(x => x.Status == FriendshipItemStatus.Accept)
                    .ToArray();
                List<int> friendsIds = friendshipAccepted
                    .Select(x => x.UserOferFrienshipSender )
                    .Where(x => x != userId)
                    .ToList();

                friendsIds.AddRange(
                    friendshipAccepted.Select(x => x.UserConfirmer)
                    .Where(x => x != userId)
                    .ToList()
                    );

                if (friendsIds.Count > 0)
                {

                    foreach (var friendId in friendsIds)
                    {
                        BaseUser userInfo = BaseUser(friendId);
                        var friend = new BaseUser()
                        {
                            UserId = friendId,
                            AvatarResizeUri = Resize.ResizedAvatarUri(userInfo.AvatarResizeUri, ModTypes.c_scale, friendAvatarResize, friendAvatarResize),
                            Name = userInfo.Name,
                            SurName = userInfo.SurName,
                            Country = userInfo.Country,
                            City = userInfo.City,
                            Age = userInfo.Age
                        };
                        bool isOnline = context.UserConnections.Any(x => x.IsActive == true && x.UserId == friendId);
                        friend.IsActive = isOnline;
                        model.Add(friend);
                    }
                }
            }
            return model;
        }

        public MyFriendsModel GetContactsBySession(string sessionId, int avatarResize = 100)
        {
            var model = new MyFriendsModel();
            model.Friends = new List<FriendModel>();
            model.IncommingInvitations = new List<FriendModel>();
            model.OutCommingInvitations = new List<FriendModel>();

            using (var context = new DataBaseContext())
            {
                int userId = UserIdBySession(sessionId);
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

                    for (int i = 0; i < FriendsConfirmIDs.Count(); i++)
                    {
                        BaseUser friendUserInfo = BaseUser(FriendsConfirmIDs[i]);
                        //int friendAges = DateTime.Now.Year - friendUserInfo.DateBirth.Year;

                        var friend = new FriendModel()
                        {
                            UserId = friendUserInfo.UserId,
                            AvatarResizeUri = Resize.ResizedAvatarUri(friendUserInfo.AvatarResizeUri, ModTypes.c_scale, avatarResize, avatarResize),
                            Name = friendUserInfo.Name,
                            SurName = friendUserInfo.SurName,
                            Country = friendUserInfo.Country,
                            City = friendUserInfo.City,
                            Age = friendUserInfo.Age
                        };
                        model.Friends.Add(friend);
                    }
                }

                if (inCommingFriendshipPending.Count() >= 1)
                {

                    for (int i = 0; i < inCommingFriendshipPending.Count(); i++)
                    {
                        BaseUser friendUserInfo = BaseUser(inCommingFriendshipPending[i].UserOferFrienshipSender);
                        var inInvite = new FriendModel()
                        {
                            UserId = friendUserInfo.UserId,
                            AvatarResizeUri = Resize.ResizedAvatarUri(friendUserInfo.AvatarResizeUri, ModTypes.c_scale, avatarResize, avatarResize),
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
                        BaseUser friendUserInfo = BaseUser(outCommingFriendshipPending[i].UserConfirmer);
                        var outInvite = new FriendModel()
                        {
                            UserId = friendUserInfo.UserId,
                            AvatarResizeUri = Resize.ResizedAvatarUri(friendUserInfo.AvatarResizeUri, ModTypes.c_scale, avatarResize, avatarResize),
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
            int userSenderId = UserIdBySession(userSenderSession);
            Guid guidID = Guid.NewGuid();
            using (var context = new DataBaseContext())
            {
                List<Guid> ConversationSenderGuids = context.ConversationGroup
                    .Where(user => user.UserId == userSenderId)
                    .Select(x => x.ConversationGuidId)
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

        public void ChangeAvatarResizeUri(string session, Uri newUri)
        {
            int userId = UserIdBySession(session);
            string uri = newUri.ToString();
            using (var context = new DataBaseContext())
            {
                var newAvatar = new Avatars();
                newAvatar.UploadTime = DateTime.UtcNow;
                newAvatar.ImgPath = uri;
                context.Avatars.Add(newAvatar);
                context.SaveChanges();

                int avatarSavedID = context.Avatars.First(x => x.ImgPath == uri).Id;

                User userInfo = context.Users.First(x => x.Id == userId);
                userInfo.AvatarId = avatarSavedID;

                context.SaveChanges();
            }
        }

        public UserSettingsModel GetSettings(string session)
        {
            var model = new UserSettingsModel();
            BaseUser user = ProfileInfo(session);
            using (var context = new DataBaseContext())
            {
                User userSett = context.Users.Where(x => x.Id == user.UserId).First();
                model.NotifyType = userSett.Settings.NotificationType;
                model.Email = userSett.Settings.Email;
                model.QuickMessage = userSett.Settings.QuickMessage;
            }
            return model;
        }

        public BaseUser AddInviteToContacts(string session, int userIDToFriendsInvite)
        {
            ProfileModel userSenderRequest = ProfileInfo(session, false);
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
                var userInfo = BaseUser(userID);
                if (userInfo == null)
                    return null;
                var secUserInfo = BaseUser(UserIdBySession(session));
                model.AvatarResizeUri = Resize.ResizedAvatarUri(userInfo.AvatarResizeUri, ModTypes.c_scale, 200, 200);
                model.Name = userInfo.Name;
                model.SurName = userInfo.SurName;
                model.Age = userInfo.Age;
                model.HelloMessage = userInfo.HelloMessage;
                model.userSearchAge = userInfo.userSearchAge;
                model.userSearchSex = userInfo.userSearchSex;
                model.purpose = userInfo.purpose;

                model.Status = FriendshipItemStatus.None;
                FriendsRelationship relationItem = context.FriendsRelationship
                    .Where(
                    x => 
                    x.UserConfirmer == secUserInfo.UserId && x.UserOferFrienshipSender == userInfo.UserId ||
                    x.UserOferFrienshipSender == secUserInfo.UserId && x.UserConfirmer == userInfo.UserId
                    ).FirstOrDefault();
                if (relationItem != null)
                {
                    model.Status = relationItem.Status;
                }
            }
            return model;
        }

        public void DropFrienship(string session, int userID)
        {
            int myId = UserIdBySession(session);
            bool isUsersFriends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(session, userID);
            if (isUsersFriends)
            {
                using (var context = new DataBaseContext())
                {
                    FriendsRelationship entryFrienship = context.FriendsRelationship
                        .Where(x => x.UserOferFrienshipSender == myId && x.UserConfirmer == userID ||
                        x.UserConfirmer == myId && x.UserOferFrienshipSender == userID)
                        .First();
                    entryFrienship.Status = FriendshipItemStatus.Close;
                    context.SaveChanges();
                }
            }
        }

        public async Task<NotifyHubModel> AcceptInviteToContacts(string session, int userID)
        {
            BaseUser accepterUser = ProfileInfo(session, false);
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
                    ProfileModel user = ProfileInfo(session);

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
                            case UserParams.UserName:
                                s_user.UserFullInfo.Name = newValue;
                                break;
                            case UserParams.UserSurname:
                                s_user.UserFullInfo.SurName = newValue;
                                break;
                            case UserParams.Country:
                                s_user.UserFullInfo.NowCountryCode = Int32.Parse(newValue);
                                break;
                            case UserParams.City:
                                s_user.UserFullInfo.NowCityCode = Int32.Parse(newValue);
                                break;
                            case UserParams.HelloStatus:
                                s_user.UserFullInfo.HelloMessage = newValue;
                                break;
                            case UserParams.DatingPurpose:
                                s_user.UserFullInfo.DatingPurpose = int.Parse(newValue);
                                break;
                            case UserParams.DatingSex:
                                s_user.UserFullInfo.userDatingSex = int.Parse(newValue);
                                break;
                            case UserParams.DatingAge:
                                s_user.UserFullInfo.userDatingAge = int.Parse(newValue);
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
                    var user = context.Users.FirstOrDefault(x => x.Settings.Email == email);
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

        public int UserIdBySession(string session)
        {
            using (var context = new DataBaseContext())
            {
                Session sess = context.Sessions.First(x => x.Number == session);
                return (int)sess.UserId;
            }
        }
    }
}