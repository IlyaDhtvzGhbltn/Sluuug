using Slug.Context.Dto.CryptoConversation;
using Slug.Model;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Messages
{
    public class NotificationModel
    {
        public BaseUser FromUser { get; set; }

        public IList<string> ConnectionIds { get; set; }

        public string Culture { get; set; }

        public dynamic PublicDataToExcange { get; set; }

        public Guid DialogId { get; set; }
    }
}