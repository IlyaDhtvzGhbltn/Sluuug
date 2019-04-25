using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.VideoConference
{
    public class IncomingInvite
    {
        public int CallerID { get; set; }

        public string CallerName { get; set; }

        public string CallerSurName { get; set; }

        public Guid ConferenceID { get; set; }
    }
}