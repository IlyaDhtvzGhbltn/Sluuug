using Context;
using Microsoft.AspNet.SignalR;
using Slug.Context;
using Slug.Context.Tables;
using Slug.Model.Users;
using Slug.Model.VideoConference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Slug.Helpers
{
    public class VideoConferenceWorker
    {
        public VideoConferenceWorker()
        {  }

        public VideoConferenceWorker(Microsoft.AspNet.SignalR.Hubs.HubCallerContext context, int calleID)
        {
            string session = context.Request.Cookies["session_id"].Value;
            var UWorker = new UserWorker();
            bool isFriends = UWorker.IsUsersAreFriends(session, calleID);
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
                var UWorker = new UserWorker();
                int userRequestedId = UWorker.GetUserInfo(sessionID).UserId;
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

            var userWorker = new UserWorker();
            int myId = userWorker.GetUserInfo(sessionID).UserId;

            MyFriendsModel fMod = userWorker.GetFriendsBySession(sessionID);
            foreach (var item in fMod.Friends)
            {
                model.Friends.Add(item);
            }

            using (var context = new DataBaseContext())
            {
                Guid[] conferenceGuids = context.VideoConferenceGroups
                    .Where(x=>x.UserId == myId)
                    .Select(x => x.GuidId)
                    .Take(100)
                    .ToArray();
                foreach (var item in conferenceGuids)
                {
                    var call = new CallModel();
                    VideoConference conference = context.VideoConferences.Where(x=>x.GuidId == item).First();
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

            return model;
        }

        public void CloseConverence(Guid guidID)
        {
            using (var context = new DataBaseContext())
            {
                VideoConference conference = context.VideoConferences.First(x=>x.GuidId == guidID);
                conference.IsActive = false;
                context.SaveChanges();
            }
        }

        public bool IsConverenceActive(Guid guidID)
        {
            bool flagStatus = false;
            using (var context = new DataBaseContext())
            {
                VideoConference conf = context.VideoConferences.First(x => x.GuidId == guidID);
                flagStatus = conf.IsActive;
            }

            return flagStatus;
        }
    }
}