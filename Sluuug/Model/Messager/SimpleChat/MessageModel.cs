using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Messager.SimpleChat
{
    public class MessageModel
    {
        public Guid ConversationId { get; set; }

        public int SenderId { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string AvatarPath { get; set; }

        public string Text { get; set; }

        public string SendTime { get; set; }

        public bool IsIncomming { get; set; }
    }
}