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
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;
using NLog;
using Slug.ImageEdit;
using Slug.Model.Users.Relations;
using Slug.Context.Dto.Search;
using System.Globalization;
using Slug.Context.Dto.UserWorker_refactor;
using Slug.DbInitialisation;
using Slug.Model.FullInfo;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Slug.Helpers.BaseController;
using Slug.Helpers.Handlers.PrivateUserServices;
using Slug.Context.Dto.Posts;

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
                    newUser.UserFullInfo = new UserInfo();
                    newUser.UserFullInfo.NowCountryCode = user.CountryCode;
                    newUser.UserFullInfo.DateOfBirth = user.DateBirth;
                    newUser.UserFullInfo.Name = user.Name;
                    newUser.UserFullInfo.SurName = user.SurName;
                    newUser.UserFullInfo.Sex = user.Sex;
                    newUser.UserFullInfo.HelloMessage = "Всем привет, я на связи!";
                    newUser.UserFullInfo.DatingPurpose = DatingPurposeEnum.NoDating;

                    newUser.Settings = new UserSettings();
                    newUser.Settings.Email = user.Email;
                    newUser.Settings.PasswordHash = Encryption.EncryptionStringToSHA512(user.PasswordHash);

                    newUser.UserType = RegisterTypeEnum.SelfUser;
                    newUser.Login = user.Login;
                    newUser.AvatarId = context.Avatars.First(x => x.CountryCode == user.CountryCode).Id;
                    newUser.UserStatus = (int)UserStatuses.AwaitConfirmation;
                    newUser.RegisterDate = DateTime.UtcNow;

                    context.Users.Add(newUser);
                    var sesWk = new SessionsHandler();
                    activationSessionId = sesWk.OpenSession(SessionTypes.AwaitEmailConfirm, 0);

                    context.SaveChanges();
                    var linkMail = new ActivationHandler();
                    List<User> User = context.Users
                        .Where(x => x.Settings.Email == user.Email).ToList();

                    activationMailParam = linkMail.CreateActivationEntries(User.Last().Id);

                    context.SaveChanges();
                    return new UserConfirmationDitails { ActivatioMailParam = activationMailParam, ActivationSessionId = activationSessionId };
                }
            }
            return null;
        }

        public int RegisterNewFromOutNetwork(OutRegisteringUserModel user, string network, RegisterTypeEnum type)
        {
            using (var context = new DataBaseContext())
            {
                context.Avatars.Add(new Avatars()
                {
                    LargeAvatar = user.Avatar200,
                    MediumAvatar = user.Avatar100,
                    SmallAvatar = user.Avatar50,
                    IsStandart = false,
                    UploadTime = DateTime.UtcNow,
                    AvatarType = AvatarTypesEnum.OutNetLoad
                });
                context.SaveChanges();
                int localUserAvatarId = context.Avatars.First(x => x.LargeAvatar == user.Avatar200).Id;

                var newUser = new User();
                newUser.Login = string.Format("{0}_{1}", network, user.OutId);
                newUser.UserStatus = (int)UserStatuses.Active;
                newUser.AvatarId = localUserAvatarId;
                newUser.UserType = type;
                newUser.RegisterDate = DateTime.UtcNow;

                newUser.UserFullInfo = new UserInfo();
                newUser.UserFullInfo.NowCountryCode = user.CountryCode;
                newUser.UserFullInfo.NowCityCode = user.CityCode;
                newUser.UserFullInfo.DateOfBirth = user.DateBirth;
                newUser.UserFullInfo.Name = user.Name;
                newUser.UserFullInfo.SurName = user.SurName;
                newUser.UserFullInfo.Sex = user.Sex;
                newUser.UserFullInfo.HelloMessage = !string.IsNullOrWhiteSpace(user.Status) ? user.Status : "Всем привет, я на связи!";
                newUser.UserFullInfo.DatingPurpose = DatingPurposeEnum.NoDating;
                if(type == RegisterTypeEnum.VkUser)
                    newUser.UserFullInfo.IdVkUser = user.OutId;
                if (type == RegisterTypeEnum.FbUser)
                    newUser.UserFullInfo.IdFBUser = user.OutId;
                if (type == RegisterTypeEnum.OkUser)
                    newUser.UserFullInfo.IdOkUser = user.OutId;

                newUser.Settings = new UserSettings();
                newUser.Settings.Email = "admin@friendlynet.ru";
                newUser.Settings.NotificationType = Context.Dto.Settings.NotificationTypes.Never;
                newUser.Settings.PasswordHash = "-1";
                newUser.Settings.QuickMessage = false;

                context.Users.Add(newUser);
                context.SaveChanges();
                int localUserFromVk = context.Users.First(x => x.AvatarId == localUserAvatarId).Id;
                return localUserFromVk;
            }
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
            string savedPassword = Crypto.Encryption.EncryptionStringToSHA512(hashPassword);
            //NewUserInitial.Initialize(10);

            using (var dbContext = new DataBaseContext())
            {
                User user = dbContext.Users
                    .FirstOrDefault(x => x.Settings.PasswordHash == savedPassword && x.Login == login && x.UserStatus == (int)UserStatuses.Active);
                if (user != null)
                    return user.Id;
            }
            return 0;
        }

        public ProfileModel ProfileInfo(string sessioID)
        {
            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.First(x => x.Number == sessioID);
                int userId = session.UserId;
                return profileInfo(userId, context);
            }
        }

        public ProfileModel ProfileInfo(int userId)
        {
            var model = new ProfileModel();
            using (var context = new DataBaseContext())
            {
                return profileInfo(userId, context);
            }
        }

        private ProfileModel profileInfo(int userId, DataBaseContext context)
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
            userModel.CountryCode = user.UserFullInfo.NowCountryCode;

            if (userCountry != null)
                userModel.Country = userCountry.Title;
            else
                userModel.Country = "Не указана";

            var City = context.Cities.Where(x => x.CitiesCode == user.UserFullInfo.NowCityCode && x.Language == LanguageType.Ru)
                .FirstOrDefault();
            userModel.CityCode = user.UserFullInfo.NowCityCode;

            if (City != null)
                userModel.City = City.Title;
            else
                userModel.City = "Не указанo";

            if (avatar.AvatarType == AvatarTypesEnum.SelfLoad)
            {
                userModel.LargeAvatar = Resize.ResizedAvatarUri(avatar.LargeAvatar, ModTypes.c_scale, 200, 200);
                userModel.MediumAvatar = Resize.ResizedAvatarUri(avatar.LargeAvatar, ModTypes.c_scale, 100, 100);
                userModel.SmallAvatar = Resize.ResizedAvatarUri(avatar.LargeAvatar, ModTypes.c_scale, 50, 50);
            }
            else
            {
                userModel.LargeAvatar = avatar.LargeAvatar;
                userModel.MediumAvatar = avatar.MediumAvatar;
                userModel.SmallAvatar = avatar.SmallAvatar;
            }

            userModel.UserId = user.Id;
            userModel.Age = DateTime.Now.Year - user.UserFullInfo.DateOfBirth.Year;

            var Educations = user.UserFullInfo.Educations;
            userModel.Educations = new List<EducationModel>();
            Educations.ForEach(x =>
            {
                string endDate = (x.End == null) ? endDate = "настоящее время" : endDate = ((DateTime)x.End).ToString("D");
                userModel.Educations.Add(new EducationModel()
                {
                    Comment = x.Comment,
                    EducationType = x.EducationType,
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
                string endDate = (x.DateEvent == null) ? endDate = "настоящее время" : endDate = (x.DateEvent).ToString("D");
                var eventModel = new MemorableEventsModel()
                {
                    Text = x.TextEventDescription,
                    DateEventFormat = endDate,
                    EventTitle = x.EventTitle,
                    Id = x.Id,
                    Photos = new List<FotoModel>(),
                    BindedAlbumId = x.AlbumGuid
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
                            string avatarUri = string.Empty;
                            if (ava.AvatarType == AvatarTypesEnum.SelfLoad)
                                avatarUri = Resize.ResizedAvatarUri(ava.LargeAvatar, ModTypes.c_scale, 50, 50);
                            else
                                avatarUri = ava.SmallAvatar;

                            commentsModel.Add(new FotoCommentModel()
                            {
                                UserName = userCommenter.UserFullInfo.Name,
                                UserSurName = userCommenter.UserFullInfo.SurName,
                                UserPostedID = userCommenter.Id,
                                UserPostedAvatarResizeUri = avatarUri,
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
                string endDate = (x.End == null) ? endDate = "настоящее время" : endDate = ((DateTime)x.End).ToString("D");

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
            string albumURI = string.Empty;
            foreach (var album in albums)
            {
                if (album.AlbumLabelUrl.Contains("system/template.jpg"))
                    albumURI = album.AlbumLabelUrl;
                else
                    albumURI = Resize.ResizedFullPhoto(album.AlbumLabelUrl, album.LabelOriginalHeight, album.LabelOriginalWidth, 90, 90);
                AlbumModel albumModel = new AlbumModel
                {
                    AlbumId = album.Id,
                    AlbumLabelUrl = albumURI,
                    AlbumDescription = album.Description,
                    CreationTime = album.CreationDate,
                    AlbumTitle = album.Title,
                    FotosCount = album.Fotos.Count
                };
                userModel.Albums.Add(albumModel);
            }

            var postHandler = new PostUserHandler();
            int oneTimePostsCountUpload = int.Parse(WebAppSettings.AppSettings[AppSettingsEnum.postsOnPage.ToString()]);

            ProfilePostModel posts = postHandler.GetMorePosts(userId, 0, context);
            if (posts.TotalPostsCount <= oneTimePostsCountUpload)
            {
                userModel.IsAllPostsUploaded = true;
            }
            userModel.Posts = new List<PostModel>();
            userModel.Posts = posts.Posts;

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

        public BaseUser BaseUser(string sessionID)
        {
            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.FirstOrDefault(x => x.Number == sessionID);
                return baseUser(session.UserId, context);
            }
        }

        public BaseUser BaseUser(int userId)
        {
            using (var context = new DataBaseContext())
            {
                return baseUser(userId, context);
            }
        }

        private BaseUser baseUser(int userId, DataBaseContext context)
        {
            var userModel = new BaseUser();
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                Avatars avatar = context.Avatars.First(x => x.Id == user.AvatarId);
                if (avatar.AvatarType == AvatarTypesEnum.SelfLoad)
                {
                    userModel.LargeAvatar = Resize.ResizedAvatarUri(avatar.LargeAvatar, ModTypes.c_scale, 200, 200);
                    userModel.MediumAvatar = Resize.ResizedAvatarUri(avatar.LargeAvatar, ModTypes.c_scale, 100, 100);
                    userModel.SmallAvatar = Resize.ResizedAvatarUri(avatar.LargeAvatar,  ModTypes.c_scale, 50, 50);
                }
                else
                {
                    userModel.LargeAvatar = avatar.LargeAvatar;
                    userModel.MediumAvatar = avatar.MediumAvatar;
                    userModel.SmallAvatar = avatar.SmallAvatar;
                }

                userModel.Name = user.UserFullInfo.Name;
                userModel.SurName = user.UserFullInfo.SurName;
                userModel.Country = context.Countries
                    .Where(x => x.CountryCode == user.UserFullInfo.NowCountryCode && x.Language == LanguageType.Ru)
                    .First()
                    .Title;

                if (user.UserFullInfo.NowCityCode != 0)
                {
                    userModel.City = context.Cities
                        .Where(x => x.CitiesCode == user.UserFullInfo.NowCityCode && x.Language == LanguageType.Ru)
                        .First()
                        .Title;
                }
                else
                    userModel.City = "не указан";

                userModel.UserType = user.UserType;
                //userModel.LargeAvatar = avatar.LargeAvatar;
                userModel.UserId = user.Id;
                userModel.Age = DateTime.Now.Year - user.UserFullInfo.DateOfBirth.Year;
                userModel.HelloMessage = user.UserFullInfo.HelloMessage;
                userModel.userSearchAge = (AgeEnum)user.UserFullInfo.userDatingAge;
                userModel.userSearchSex = (SexEnum)user.UserFullInfo.userDatingSex;
                userModel.purpose = (DatingPurposeEnum)user.UserFullInfo.DatingPurpose;
            }
            else
            {
                return null;
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

        public async Task<List<BaseUser>> GetFriendsOnlyBySession(string sessionId)
        {
            int userId = UserIdBySession(sessionId);
            var model = new List<BaseUser>();

            using (var context = new DataBaseContext())
            {
                UsersRelation[] friendshipAccepted = context.UserRelations
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
                    var videoHandler = new VideoConferenceHandler();
                    foreach (int friendId in friendsIds)
                    {
                        BaseUser userInfo = BaseUser(friendId);
                        if (userInfo != null)
                        {
                            var friend = new BaseUser()
                            {
                                UserId = friendId,
                                LargeAvatar = userInfo.MediumAvatar,
                                Name = userInfo.Name,
                                SurName = userInfo.SurName,
                                Country = userInfo.Country,
                                City = userInfo.City,
                                Age = userInfo.Age
                            };
                            bool alreadyStart = videoHandler.AlreadyStart(context, friendId, friendId);
                            friend.IsOnline = IsOnline(context, friendId).GetAwaiter().GetResult();
                            if (!friend.IsOnline)
                                friend.AcceptToInfite = VideoConverenceAcceptToCall.offline;
                            if (friend.IsOnline && !alreadyStart)
                                friend.AcceptToInfite = VideoConverenceAcceptToCall.online;
                            if (friend.IsOnline && alreadyStart)
                                friend.AcceptToInfite = VideoConverenceAcceptToCall.pending;
                            model.Add(friend);
                        }
                    }
                }
            }
            return model;
        }

        public async Task<ContactsModel> GetContactsBySession(string sessionId, int avatarResize = 100)
        {
            var model = new ContactsModel();
            model.Friends = new List<BaseUser>();
            model.IncommingInvitations = new List<BaseUser>();
            model.OutCommingInvitations = new List<BaseUser>();
            model.BlockedUser = new List<BlockedUser>();

            using (var context = new DataBaseContext())
            {
                int userId = UserIdBySession(sessionId);
                await InvitationSeen(context, userId);

                UsersRelation[] friendshipAccepted = context.UserRelations
                    .Where(x => x.UserOferFrienshipSender == userId || x.UserConfirmer == userId)
                    .Where(x => x.Status == FriendshipItemStatus.Accept)
                    .ToArray();

                if (friendshipAccepted.Count() >= 1)
                {
                    var confirmerIds = friendshipAccepted.Where(x => x.UserConfirmer != userId).Select(x => x.UserConfirmer);
                    var acceptedIds = friendshipAccepted.Where(x => x.UserOferFrienshipSender != userId).Select(x => x.UserOferFrienshipSender);
                    var FriendsConfirmIDs = confirmerIds.Concat(acceptedIds).ToArray();

                    for (int i = 0; i < FriendsConfirmIDs.Count(); i++)
                    {
                        BaseUser friendUserInfo = BaseUser(FriendsConfirmIDs[i]);
                        if (friendUserInfo != null)
                        {
                            var friend = new BaseUser()
                            {
                                UserId = friendUserInfo.UserId,
                                LargeAvatar = friendUserInfo.MediumAvatar,
                                Name = friendUserInfo.Name,
                                SurName = friendUserInfo.SurName,
                                Country = friendUserInfo.Country,
                                City = friendUserInfo.City,
                                Age = friendUserInfo.Age,
                                HelloMessage = friendUserInfo.HelloMessage
                            };
                            friend.IsOnline = IsOnline(context, friendUserInfo.UserId).GetAwaiter().GetResult();
                            model.Friends.Add(friend);
                        }
                    }
                }
                UsersRelation[] inCommingFriendshipPending = context.UserRelations
                    .Where(x => x.UserConfirmer == userId)
                    .Where(x => x.Status == FriendshipItemStatus.Pending)
                    .ToArray();

                if (inCommingFriendshipPending.Count() >= 1)
                {

                    for (int i = 0; i < inCommingFriendshipPending.Count(); i++)
                    {
                        BaseUser friendUserInfo = BaseUser(inCommingFriendshipPending[i].UserOferFrienshipSender);
                        if (friendUserInfo != null)
                        {
                            var inInvite = new BaseUser()
                            {
                                UserId = friendUserInfo.UserId,
                                LargeAvatar = friendUserInfo.MediumAvatar,
                                Name = friendUserInfo.Name,
                                SurName = friendUserInfo.SurName,
                                HelloMessage = friendUserInfo.HelloMessage,
                                Country = friendUserInfo.Country,
                                City = friendUserInfo.City,
                                Age = friendUserInfo.Age,
                            };

                            inInvite.IsOnline = IsOnline(context, friendUserInfo.UserId).GetAwaiter().GetResult();
                            model.IncommingInvitations.Add(inInvite);
                        }
                    }
                }

                UsersRelation[] outCommingFriendshipPending = context.UserRelations
                    .Where(x => x.UserOferFrienshipSender == userId)
                    .Where(x => x.Status == FriendshipItemStatus.Pending)
                    .ToArray();

                if (outCommingFriendshipPending.Count() >= 1)
                {
                    for (int i = 0; i < outCommingFriendshipPending.Count(); i++)
                    {
                        BaseUser friendUserInfo = BaseUser(outCommingFriendshipPending[i].UserConfirmer);
                        if (friendUserInfo != null)
                        {
                            var outInvite = new BaseUser()
                            {
                                UserId = friendUserInfo.UserId,
                                LargeAvatar = friendUserInfo.MediumAvatar,
                                Name = friendUserInfo.Name,
                                SurName = friendUserInfo.SurName,
                                Country = friendUserInfo.Country,
                                City = friendUserInfo.City,
                                Age = friendUserInfo.Age,
                                HelloMessage = friendUserInfo.HelloMessage
                            };

                            outInvite.IsOnline = IsOnline(context, outInvite.UserId).GetAwaiter().GetResult();
                            model.OutCommingInvitations.Add(outInvite);
                        }
                    }
                }

                List<BlockedUsersEntries> blockedUsers = context.BlackList.Where(x => x.UserBlocker == userId).ToList();
                if (blockedUsers != null)
                {
                    foreach (var item in blockedUsers)
                    {
                        BaseUser blockUser = BaseUser(item.UserBlocked);
                        if (blockUser != null)
                        {
                            model.BlockedUser.Add(new BlockedUser()
                            {
                                UserId = item.UserBlocked,
                                HateMessage = item.HateMessage,
                                LargeAvatar = blockUser.MediumAvatar,
                                Name = blockUser.Name,
                                SurName = blockUser.SurName,
                                BlockDate = item.BlockDate
                            });
                        }
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
        }

        public void ChangeAvatarResizeUri(string session, Uri newUri)
        {
            int userId = UserIdBySession(session);
            string uri = newUri.ToString();
            using (var context = new DataBaseContext())
            {
                var newAvatar = new Avatars();
                newAvatar.UploadTime = DateTime.UtcNow;
                newAvatar.LargeAvatar = uri;
                context.Avatars.Add(newAvatar);
                context.SaveChanges();

                int avatarSavedID = context.Avatars.First(x => x.LargeAvatar == uri).Id;

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
            BaseUser userSenderRequest = BaseUser(session);
            using (var context = new DataBaseContext())
            {
                User invitedUser = context.Users.FirstOrDefault(x => x.Id == userIDToFriendsInvite);
                if (invitedUser != null)
                {
                    UsersRelation invitationAlreadySand = 
                        FriendshipChecker.GetRelation(context, userSenderRequest.UserId, userIDToFriendsInvite);
                    if (invitationAlreadySand == null || invitationAlreadySand.Status == FriendshipItemStatus.None)
                    {
                        var relation = new UsersRelation();
                        relation.OfferSendedDate = DateTime.UtcNow;
                        relation.UserOferFrienshipSender = userSenderRequest.UserId;
                        relation.UserConfirmer = invitedUser.Id;
                        relation.Status = FriendshipItemStatus.Pending;
                        relation.IsInvitationSeen = false;
                        context.UserRelations.Add(relation);
                        context.SaveChanges();
                        return userSenderRequest;
                    }
                    else
                    {
                        invitationAlreadySand.IsInvitationSeen = false;
                        context.SaveChanges();
                        return userSenderRequest;
                    }
                }
            }
            return null;
        }

        public BaseUser GetForeignUserInfo(string session, int userObjectRequestId)
        {
            var model = new BaseUser();
            using (var context = new DataBaseContext())
            {
                BaseUser someUser = BaseUser(userObjectRequestId);
                BaseUser Iam = BaseUser(session);

                if (someUser == null || Iam == null)
                {
                    return null;
                }

                model.LargeAvatar = someUser.LargeAvatar;
                model.Name = someUser.Name;
                model.SurName = someUser.SurName;
                model.Age = someUser.Age;
                model.HelloMessage = someUser.HelloMessage;
                model.userSearchAge = someUser.userSearchAge;
                model.userSearchSex = someUser.userSearchSex;
                model.purpose = someUser.purpose;
                model.UserId = someUser.UserId;
                model.City = someUser.City;
                model.Country = someUser.Country;

                UsersRelation relationItem = FriendshipChecker.GetRelation(context, Iam.UserId, userObjectRequestId);
                BlockedUsersEntries blockItem = FriendshipChecker.GetBlockRelation(context, Iam.UserId, userObjectRequestId);

                if (relationItem != null)
                {
                if (relationItem.UserOferFrienshipSender == Iam.UserId)
                    model.Status = FriendshipItemStatus.IInviteUserToContact;
                if (relationItem.UserConfirmer == Iam.UserId)
                    model.Status = FriendshipItemStatus.UserInviteMeToContact;
                }
                if (blockItem != null)
                {
                    var blockUser = new BlockedUser();
                    blockUser.BlockDate = blockItem.BlockDate;
                    blockUser.LargeAvatar = Resize.ResizedAvatarUri(someUser.LargeAvatar, ModTypes.c_scale, 200, 200);
                    blockUser.Name = someUser.Name;
                    blockUser.SurName = someUser.SurName;
                    blockUser.Age = someUser.Age;
                    blockUser.UserId = someUser.UserId;
                    if (blockItem.UserBlocker == Iam.UserId)
                    {
                        blockUser.Status = FriendshipItemStatus.IblockUser;
                    }
                    if (blockItem.UserBlocked == Iam.UserId)
                    {
                        blockUser.HateMessage = blockItem.HateMessage;
                        blockUser.Status = FriendshipItemStatus.UserBlockMe;
                    }
                    return blockUser;
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
                    UsersRelation entryFrienship = context.UserRelations
                        .Where(x => x.UserOferFrienshipSender == myId && x.UserConfirmer == userID ||
                        x.UserConfirmer == myId && x.UserOferFrienshipSender == userID)
                        .First();
                    context.UserRelations.Remove(entryFrienship);
                    context.SaveChanges();
                }
            }
        }

        public async Task BlockUser(int userBlocker, BlockContactRequest request)
        {
            using (var context = new DataBaseContext())
            {

                Guid blockId = Guid.NewGuid();
                var block = new BlockedUsersEntries()
                {
                    BlockEntryId = blockId,
                    BlockDate = DateTime.Now,
                    HateMessage = request.HateMessage,
                    UserBlocker = userBlocker,
                    UserBlocked = request.BlockedUserId
                };

                context.BlackList.Add(block);
                var relations = FriendshipChecker.GetRelation(context, userBlocker, request.BlockedUserId);
                if(relations != null)
                    relations.BlockEntrie = blockId;

                await context.SaveChangesAsync();
            }
        }

        public async Task UnblockUser(int userBlocker, UnblockContactRequest request)
        {
            using (var context = new DataBaseContext())
            {
                var blockEntrie = FriendshipChecker.GetBlockRelation(context, userBlocker, request.UserNeedUnblockId);
                context.BlackList.Remove(blockEntrie);
                await context.SaveChangesAsync();
            }
        }

        public async Task<NotificationModel> AcceptInviteToContacts(string session, int userID)
        {
            BaseUser accepterUser = ProfileInfo(session);
            using (var context = new DataBaseContext())
            {
                UsersRelation item = context.UserRelations
                    .Where(x => x.Status == FriendshipItemStatus.Pending &&
                    x.UserOferFrienshipSender == userID &&
                    x.UserConfirmer == accepterUser.UserId)
                    .First();

                item.Status = FriendshipItemStatus.Accept;
                context.SaveChanges();

                var connectionHandler = new UsersConnectionHandler();
                var connections = connectionHandler.GetConnectionById(userID);
                var responce = new NotificationModel();
                responce.ConnectionIds = connections.ConnectionId;
                responce.FromUser = accepterUser;
                responce.Culture = connections.CultureCode[0];
                return responce;
            }
        }

        public ChangeParameterResponce ChangeParameter(string session, UserParams parameter, string newValue, string additionParameter)
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
                            //case UserParams.Country:
                            //    s_user.UserFullInfo.NowCountryCode = Int32.Parse(newValue);
                            //    break;
                            case UserParams.City:
                                s_user.UserFullInfo.NowCityCode = Int32.Parse(newValue);
                                s_user.UserFullInfo.NowCountryCode = Int32.Parse(additionParameter);
                                break;
                            case UserParams.HelloStatus:
                                s_user.UserFullInfo.HelloMessage = newValue;
                                break;
                            case UserParams.DatingPurpose:
                                s_user.UserFullInfo.DatingPurpose = (DatingPurposeEnum)int.Parse(newValue);
                                break;
                            case UserParams.DatingSex:
                                s_user.UserFullInfo.userDatingSex = (SexEnum)int.Parse(newValue);
                                break;
                            case UserParams.DatingAge:
                                s_user.UserFullInfo.userDatingAge = (AgeEnum)int.Parse(newValue);
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
                return sess.UserId;
            }
        }

        public async Task<bool> IsOnline(DataBaseContext context, int userId)
        {
            bool flagOnline = false;
            UserConnections entry = context.UserConnections.FirstOrDefault(x => x.IsActive == true && x.UserId == userId);
            if (entry != null)
            {
                TimeSpan updateInterval = DateTime.Now.Subtract(entry.UpdateTime);
                if (updateInterval.TotalSeconds > 30)
                {
                    entry.UpdateTime = DateTime.Now;
                    entry.IsActive = false;
                    context.SaveChanges();

                }
                else
                    flagOnline = true;
            }
            return flagOnline;
        }

        public bool IsOnline(int userId)
        {
            return isOnline(userId);
        }

        private bool isOnline(int userId)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                bool flag = context.UserConnections.Any(x => x.IsActive == true && x.UserId == userId);
                return flag;
            }
        }
        

        private async Task InvitationSeen(DataBaseContext context, int userId)
        {
            var relation = context.UserRelations.Where(x =>
                x.IsInvitationSeen == false &&
                x.UserConfirmer == userId)
                .ToList();

            if(relation.Count > 0)
            {
                relation.ForEach(r =>
                {
                    r.IsInvitationSeen = true;
                });
            }
            await context.SaveChangesAsync();
        }
    }
}