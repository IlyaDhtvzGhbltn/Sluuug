using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.FullInfo
{
    public class UserInfoItem
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public DateTime Start { get; set; }

        public string StartDateFormat { get; set; }

        public DateTime? End { get; set; }

        public string EndDateFormat { get; set; }

        public bool UntilNow { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
    }
}