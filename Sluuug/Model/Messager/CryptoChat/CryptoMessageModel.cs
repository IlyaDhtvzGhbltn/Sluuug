using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Messager.CryptoChat
{
    public class CryptoMessageModel
    {
        public Guid DialogId { get; set; }
        public int UserSenderId { get; set; }
        public bool IsIncomming { get; set; }

        public string AvatatURI { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string Text { get; set; }

        public DateTime SendDate { get; set; }
    }
}