using Slug.Context.Dto.UserFullInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.FullInfo
{
    public class BaseInfoModel : EntryBase
    {
        public string Country { get; set; }

        public string Sity { get; set; }

        public bool UntilNow { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public string Comment { get; set; }

        public int PersonalRating { get; set; }
    }
}