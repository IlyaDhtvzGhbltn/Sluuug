using Context;
using Slug.Context;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Slug.Helpers
{
    public class VideoConferenceWorker
    {
        public void UpdateConferenceOffer(string offer, int creatorId, Guid guidID)
        {
            using (var context = new DataBaseContext())
            {
                var conference = new VideoConference();
                conference.GuidId = guidID;
                conference.CreationDate = DateTime.UtcNow;
                conference.Offer = offer;
                conference.OfferSenderUser = creatorId;
                conference.ConferenceCreatorUserId = creatorId;
                //conference.AnswerSenderUser = conversationParticipant;
                context.VideoConferences.Add(conference);

                var conferenceGroup = new VideoConferenceGroups();

                conferenceGroup.GuidId = guidID;
                //conferenceGroup.UserId = conversationParticipant;
                context.VideoConferenceGroups.Add(conferenceGroup);

                conferenceGroup = new VideoConferenceGroups();
                conferenceGroup.GuidId = guidID;
                conferenceGroup.UserId = creatorId;
                context.VideoConferenceGroups.Add(conferenceGroup);

               context.SaveChanges();
            }
        }

        public Guid Create(int creatorId, int conversationParticipant)
        {
            var guidID = Guid.NewGuid();
            using (var context = new DataBaseContext())
            {
                var conference = new VideoConference();
                conference.GuidId = guidID;
                conference.CreationDate = DateTime.UtcNow;
                conference.OfferSenderUser = creatorId;
                conference.AnswerSenderUser = conversationParticipant;
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
    }
}