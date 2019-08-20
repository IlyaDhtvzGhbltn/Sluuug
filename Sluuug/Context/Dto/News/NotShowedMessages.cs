using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.News
{
    public class NotShowedNews
    {
        public Dictionary<string, int> NotReadedConversations { get; set; }

        public Dictionary<string, int> NotReadedCryptoConversations { get; set; }

        public int NewInviteToContacts { get; set; }
    }
}