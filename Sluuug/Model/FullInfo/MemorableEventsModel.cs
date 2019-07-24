using Slug.Context.Dto.UserFullInfo;
using Slug.Model.FullInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class MemorableEventsModel : UserInfoItem
    {
        public string EventTitle { get; set; }

        public DateTime? DateEvent { get; set; }
    }
}