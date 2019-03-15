using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class MessageModel
    {
        public string SessionNumber { get; set; }

        public string ConversationId { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }
    }
}