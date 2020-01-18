using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.VideoConference
{
    public class VideoConferenceModel
    {
        public bool IsIncommingExist { get; set; }

        public List<VideoConferenceUser> Friends { get; set; }

        //public List<CallModel> CallsHistory { get; set; }

        public List<IncomingInviteModel> IncomingCalls { get; set; }
    }
}