using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.SimpleChat
{
    public class CutConversation
    {
        public Guid GuidId { get; set; }

        public string LastMessageSenderName { get; set; }

        public string LastMessageSenderSurName { get; set; }

        public string LastMessageSenderAvatar { get; set; }

        public string InterlocutorName { get; set; }

        public string InterlocutorSurName { get; set; }

        public string InterlocutorAvatar { get; set; }

        public string LastMessage { get; set; }
    }
}