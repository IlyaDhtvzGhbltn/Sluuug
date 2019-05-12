using Context;
using Microsoft.AspNet.SignalR;
using Slug.Context;
using Slug.Context.Tables;
using Slug.Helpers.BaseController;
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
                var UWorker = new UsersHandler();
                int userRequestedId = UWorker.GetFullUserInfo(sessionID).UserId;
                if (userRequestedId == userCreatorConferenceID)
                    return VideoConverenceCallType.Caller;
                else
                    return VideoConverenceCallType.Calle;
            }
        }

        public VideoConferenceModel VideoConferenceModel (string sessionID)
        {
            var model = new VideoConferenceModel();
            model.Friends = new List<FriendModel>();
            model.CallsHistory = new List<CallModel>();
            model.IncomingCalls = new List<IncomingInvite>();

            var userWorker = new UsersHandler();
            int myId = userWorker.GetFullUserInfo(sessionID).UserId;

            MyFriendsModel fMod = userWorker.GetFriendsBySession(sessionID);
            foreach (var item in fMod.Friends)
            {
                model.Friends.Add(item);
            }

            using (var context = new DataBaseContext())
            {
                Guid[] conferenceHistoryIDs = context.VideoConferenceGroups
                    .OrderByDescending(x => x.Id)
                    .Where(x=>x.UserId == myId)
                    .Select(x => x.GuidId)
                    .Take(100)
                    .ToArray();
                foreach (var item in conferenceHistoryIDs)
                {
                    var call = new CallModel();
                    VideoConference conference = context.VideoConferences
                        .Where(x=>x.GuidId == item && x.IsActive == false)
                        .FirstOrDefault();
                    if (conference != null)
                    {
                        call.CallerUserId = conference.ConferenceCreatorUserId;
                        call.Date = conference.CreationDate;
                        if (call.CallerUserId == myId)
                            call.State = CallState.Out;
                        else
                            call.State = CallState.In;

                        int calleId = context.VideoConferenceGroups
                            .Where(x => x.GuidId == item && x.UserId != myId)
                            .First().UserId;
                        call.CalleUserId = calleId;
                        model.CallsHistory.Add(call);
                    }
                }

                List<VideoConference> incomingCalls = context.VideoConferences
                    .Where(x => x.IsActive == true)
                    .ToList();

                foreach (var item in incomingCalls)
                {
                    var incoming = new IncomingInvite();
                    incoming.ConferenceID = item.GuidId;
                    int participantID = context.VideoConferenceGroups
                        .Where(x => x.GuidId == item.GuidId && x.UserId != myId)
                        .Select(x => x.UserId)
                        .First();
                    incoming.CallerID = participantID;
                    incoming.CallerName = userWorker.GetUserInfo(participantID).Name;
                    incoming.CallerSurName = userWorker.GetUserInfo(participantID).SurName;

                    model.IncomingCalls.Add(incoming);
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