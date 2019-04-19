using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.VideoConference
{
    public class CallModel
    {
        public int CallerUserId { get; set; }

        public int CalleUserId { get; set; }

        public DateTime Date { get; set; }

        public CallState State { get; set; }
    }
}