using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.News
{
    public class NotShowedMessages
    {
        public int NotReadedConversations { get; set; }

        public int NotReadedCryptoConversations { get; set; }
    }
}