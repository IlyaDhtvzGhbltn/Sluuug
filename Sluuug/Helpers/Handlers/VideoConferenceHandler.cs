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

        public Guid Create(int creatorId, int conversationParticipant)
        {
            var guidID = Guid.NewGuid();
            using (var context = new DataBaseContext())
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
            }
            return guidID;
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

        public VideoConferenceModel VideoConferenceModel (string sessionID)
        {
            var model = new VideoConferenceModel();
            model.Friends = new List<BaseUser>();
            //model.CallsHistory = new List<CallModel>();
            model.IncomingCalls = new List<IncomingInviteModel>();

            var usersHandler = new UsersHandler();
            int myId = usersHandler.UserIdBySession(sessionID);

            var fMod = usersHandler.GetFriendsOnlyBySession(sessionID, 80);
            foreach (var item in fMod)
            {
                model.Friends.Add(item);
            }

            using (var context = new DataBaseContext())
            {
                //Guid[] conferenceHistoryIDs = context.VideoConferenceGroups
                //    .OrderByDescending(x => x.Id)
                //    .Where(x=>x.UserId == myId)
                //    .Select(x => x.GuidId)
                //    .Take(100)
                //    .ToArray();
                //foreach (var item in conferenceHistoryIDs)
                //{
                //    var call = new CallModel();
                //    VideoConference conference = context.VideoConferences
                //        .Where(x=>x.GuidId == item && x.IsActive == false)
                //        .FirstOrDefault();
                //    if (conference != null)
                //    {
                //        call.CallerUserId = conference.ConferenceCreatorUserId;
                //        call.Date = conference.CreationDate;
                //        if (call.CallerUserId == myId)
                //            call.State = CallState.Out;
                //        else
                //            call.State = CallState.In;

                //        int calleId = context.VideoConferenceGroups
                //            .Where(x => x.GuidId == item && x.UserId != myId)
                //            .First().UserId;
                //        call.CalleUserId = calleId;
                //        model.CallsHistory.Add(call);
                //    }
                //}

                List<VideoConferenceGroups> userConverense = context.VideoConferenceGroups
                    .Where(x => x.UserId == myId)
                    .ToList();

                List<VideoConference> incomingCalls = context.VideoConferences
                    .Where(x => x.IsActive == true)
                    .ToList();
                if (incomingCalls.Count > 0)
                {
                    model.IsIncommingExist = true;
                    foreach (var item in incomingCalls)
                    {
                        var incoming = new IncomingInviteModel();
                        incoming.ConferenceID = item.GuidId;
                        int participantID = context.VideoConferenceGroups
                            .Where(x => x.GuidId == item.GuidId && x.UserId != myId)
                            .Select(x => x.UserId)
                            .First();
                        var info = usersHandler.BaseUser(participantID);

                        incoming.InviterID = participantID;
                        incoming.CallerName = usersHandler.BaseUser(participantID).Name;
                        incoming.CallerSurName = usersHandler.BaseUser(participantID).SurName;
                        incoming.AvatarResizeUri = Resize.ResizedAvatarUri(info.AvatarResizeUri, ModTypes.c_scale, 50, 50);
                        model.IncomingCalls.Add(incoming);
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
    }
}