using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Slug.Model.VideoConference
{
    public class IncomingInviteModel
    {
        [JsonProperty("conferenceID")]
        public Guid ConferenceID { get; set; }

        [JsonProperty("inviterId")]
        public int InviterID { get; set; }

        public string CallerName { get; set; }

        public string CallerSurName { get; set; }

        public string AvatarUri { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }
    }
}