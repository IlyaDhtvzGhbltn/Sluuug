using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.VideoConference
{
    public class VideoConferenceModel
    {
        public List<FriendModel> Friends { get; set; }

        public List<CallModel> CallsHistory { get; set; }

        public List<IncomingInvite> IncomingCalls { get; set; }
    }
}