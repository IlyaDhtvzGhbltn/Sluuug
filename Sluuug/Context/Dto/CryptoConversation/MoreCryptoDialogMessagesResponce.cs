using Slug.Model.Messager.CryptoChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.CryptoConversation
{
    public class MoreCryptoDialogMessagesResponce
    {
        public List<CryptoMessageModel> Messages { get; set; }
        public int DialogTotalMessageCount { get; set; }
    }
}