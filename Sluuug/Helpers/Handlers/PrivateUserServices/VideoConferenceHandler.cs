using Context;
using Microsoft.AspNet.SignalR;
using Slug.Context;
using Slug.Context.Tables;
using Slug.Helpers.BaseController;
using Slug.ImageEdit;
using Slug.Model.Users;
using Slug.Model.VideoConference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using SharedModels.Enums;
using SharedModels.Users;

namespace Slug.Helpers
{
    public class VideoConferenceHandler
    {
        public VideoConferenceHandler()
        {  }

        public VideoConferenceHandler(Microsoft.AspNet.SignalR.Hubs.HubCallerContext context, int calleID)
        {
            string session = context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            var UWorker = new UsersHandler();
            bool isFriends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(session, calleID);
            if (!isFriends)
                throw new Exception("Users Are Not Friends");
        }

        public string Create(int creatorId, int conversationParticipant)
        {
            var guidID = Guid.NewGuid();
            using (var context = new DataBaseContext())
            {
                bool alreadyStart = AlreadyStart(context, creatorId, conversationParticipant);
                if (!alreadyStart)
                {
                    var conference = new VideoConference();
                    conference.IsActive = true;
                    conference.GuidId = guidID;
                    conference.CreationDate = DateTime.UtcNow;
                    conference.ConferenceCreatorUserId = creatorId;
                    context.VideoConferences.Add(conference);

                    var conferenceGroup = new VideoConferenceGroups();

                    conferenceGroup.GuidId = guidID;
                    conferenceGroup.UserId = conversationParticipant;
                    context.VideoConferenceGroups.Add(conferenceGroup);

                    conferenceGroup = new VideoConferenceGroups();
                    conferenceGroup.GuidId = guidID;
                    conferenceGroup.UserId = creatorId;
                    context.VideoConferenceGroups.Add(conferenceGroup);

                    context.SaveChanges();
                    return guidID.ToString();
                }
            }
            return null;
        }

        public void UpdateConferenceOffer(string offer, int creatorId, Guid guidID)
        {
            using (var context = new DataBaseContext())
            {
                VideoConference conference = context.VideoConferences.First(x=>x.GuidId == guidID);
                conference.Offer = offer;
                context.SaveChanges();
            }
        }

        public void SaveAnswerVideoConference(string answer, Guid guidID)
        {
            using (var context = new DataBaseContext())
            {
                VideoConference entry = context.VideoConferences.First(x=>x.GuidId == guidID);
                entry.Answer = answer;
                context.SaveChanges();
            }
        }

        public VideoConverenceCallType UserVCType(string sessionID, Guid videoConverenceID)
        {
            using (var context = new DataBaseContext())
            {
                int userCreatorConferenceID = context.VideoConferences.First(x=>x.GuidId == videoConverenceID).ConferenceCreatorUserId;
                var usersHandler = new UsersHandler();
                int userRequestedId = usersHandler.UserIdBySession(sessionID);
                if (userRequestedId == userCreatorConferenceID)
                    return VideoConverenceCallType.Caller;
                else
                    return VideoConverenceCallType.Calle;
            }
        }

        public List<VideoConference> ActiveVideoConverence(DataBaseContext context, int userId)
        {
            List<Guid> allUserConferences = context.VideoConferenceGroups
                .Where(x => x.UserId == userId)
                .Select(x => x.GuidId)
                .ToList();

            List<VideoConference> activeConference = context.VideoConferences
                .Where(x => allUserConferences.Contains(x.GuidId))
                .Where(x => x.IsActive == true)
                .ToList();

            return activeConference;
        }

        public async Task<VideoConferenceModel> VideoConferenceModel (string sessionID)
        {
            var usersHandler = new UsersHandler();
            var vipHandler = new VipUsersHandler();
            var model = new VideoConferenceModel();

            model.Friends = new List<VideoConferenceUser>();
            model.IncomingCalls = new List<IncomingInviteModel>();

            using (var context = new DataBaseContext())
            {
                int myId = usersHandler.UserIdBySession(sessionID);
                var friendshipAccepted = context.UserRelations
                    .Where(x => x.UserOferFrienshipSender.Id == myId || x.UserConfirmer.Id == myId)
                    .Where(x => x.Status == FriendshipItemStatus.Accept)
                    .Select((f) => new { f.UserConfirmer, f.UserOferFrienshipSender })
                    .ToList();

                var frIds = new List<int>();
                friendshipAccepted.ForEach(item =>
                {
                    if (item.UserConfirmer.Id != myId)
                        frIds.Add(item.UserConfirmer.Id);
                    else
                        frIds.Add(item.UserOferFrienshipSender.Id);
                });
                frIds.ForEach(id=> 
                {
                    var user = usersHandler.BaseUser(id, context);
                    model.Friends.Add(new VideoConferenceUser()
                    {
                        UserId = id,
                        Age = user.Age,
                        Name = user.Name,
                        Surname = user.SurName,
                        City = user.City,
                        Country = user.Country,
                        IsOnline = usersHandler.IsOnline(context, id).GetAwaiter().GetResult(),
                        IsVip = user.Vip,
                        Avatar = user.LargeAvatar,
                        AvaliableVipContact = vipHandler.SenderAvaliableContact(myId, id)
                    });
                });



                List <VideoConference> activeConference = ActiveVideoConverence(context, myId);
                if (activeConference.Count > 0)
                {
                    foreach (var item in activeConference)
                    {
                        var incoming = new IncomingInviteModel();
                        incoming.ConferenceID = item.GuidId;
                        int participantID = context.VideoConferenceGroups
                            .Where(x => x.GuidId == item.GuidId && x.UserId != myId)
                            .Select(x => x.UserId)
                            .First();
                        BaseUser info = usersHandler.BaseUser(participantID);
                        if (info != null)
                        {
                            model.IsIncommingExist = true;

                            incoming.InviterID = participantID;
                            incoming.CallerName = usersHandler.BaseUser(participantID).Name;
                            incoming.CallerSurName = usersHandler.BaseUser(participantID).SurName;
                            incoming.AvatarResizeUri = info.MediumAvatar;
                            model.IncomingCalls.Add(incoming);
                        }
                    }
                }
            }
            return model;
        }

        public void CloseConverence(Guid ID)
        {
            using (var context = new DataBaseContext())
            {
                VideoConference conference = context.VideoConferences.First(x=>x.GuidId == ID);
                conference.IsActive = false;
                context.SaveChanges();
            }
        }

        public bool IsConverenceActive(Guid ID)
        {
            bool flagStatus = false;
            using (var context = new DataBaseContext())
            {
                VideoConference conf = context.VideoConferences.First(x => x.GuidId == ID);
                flagStatus = conf.IsActive;
            }

            return flagStatus;
        }

        public int[] GetVideoConferenceParticipantsIDs(Guid ID)
        {
            using (var context = new DataBaseContext())
            {
                int[] users = context.VideoConferenceGroups
                    .Where(x => x.GuidId == ID)
                    .Select(x => x.UserId).
                    ToArray();
                return users;
            }
        }

        public int GetVideoConferenceParticipantID(Guid videoConferenceID, int insteadUserID)
        {
            using (var context = new DataBaseContext())
            {
                return context.VideoConferenceGroups
                    .Where(x => x.GuidId == videoConferenceID && x.UserId != insteadUserID)
                    .Select(x => x.UserId)
                    .First();
            }
        }

        public void CloseAllConferencesUserExit(DataBaseContext context, int userExit)
        {
            List<VideoConference> allVideoConferences = ActiveVideoConverence(context, userExit);
            var usersInVideoConferences = new List<int>();
            allVideoConferences.ForEach(x => 
            {
                int otherUserInConference = context.VideoConferenceGroups
                    .First(c => 
                    c.GuidId == x.GuidId && c.UserId != userExit).UserId;
                usersInVideoConferences.Add(otherUserInConference);
            });

            var userHandler = new UsersHandler();
            bool someOneOnlineFlag = false;
            usersInVideoConferences.ForEach(user =>
            {
                bool isOnline = userHandler.IsOnline(context, user).GetAwaiter().GetResult();
                if (isOnline)
                    someOneOnlineFlag = true;
            });
            if (usersInVideoConferences.Count != 0 && !someOneOnlineFlag)
            {
                allVideoConferences.ForEach(c => CloseConverence(c.GuidId));
            }
        }


        public bool AlreadyStart(DataBaseContext context, int firstUser, int secondUser)
        {
            List<VideoConference> activeConferenceFirst = ActiveVideoConverence(context, firstUser);
            List<VideoConference> activeConferenceSecond = ActiveVideoConverence(context, secondUser);
            if (activeConferenceFirst.Any(x => activeConferenceSecond.Contains(x)))
                return true;
            else 
                return false;
        }
    }
}