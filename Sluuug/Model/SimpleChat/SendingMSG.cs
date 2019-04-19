using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class SendingMSG
    {
        public string SessionId { get; set; }

        public string Message { get; set; }

        public int ConversationId { get; set; }
    }
}