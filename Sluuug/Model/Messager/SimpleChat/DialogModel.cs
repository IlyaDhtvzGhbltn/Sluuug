using Slug.Model.Messager.SimpleChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class DialogModel
    {
        public string Interlocutor { get; set; }

        public Guid DialogId { get; set; }

        public int PagesCount { get; set; }

        public List<MessageModel> Messages { get; set; }
    }
}