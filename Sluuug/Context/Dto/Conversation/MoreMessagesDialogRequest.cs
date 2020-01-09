using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Conversation
{
    public class MoreMessagesDialogRequest
    {
        public Guid DialogId { get; set; }
        public int LoadedMessages { get; set; }
        public int UserId { get; set; }
    }
}