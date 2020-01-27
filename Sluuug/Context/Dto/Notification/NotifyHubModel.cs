using System;
using System.Collections.Generic;
using SharedModels.Users;


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