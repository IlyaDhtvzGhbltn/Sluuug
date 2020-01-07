using Slug.Model.Messager.SimpleChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Conversation
{
    public class MoreMessegesDialogResponce
    {
        public List<MessageModel> Messages { get; set; }
        public int DialogMessageCount { get; set; }
    }
}