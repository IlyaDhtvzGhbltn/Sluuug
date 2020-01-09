using Slug.Model.Messager.CryptoChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class CryptoDialogModel
    {
        public Guid GuidId { get; set; }
        public int MinsLeft { get; set; }
        public int SecLeft { get; set; }
        public int TotalDialogMessagesCount { get; set; }
        public List<CryptoMessageModel> Messages { get; set; }
        public bool NotExpired { get; set; }
    }

}