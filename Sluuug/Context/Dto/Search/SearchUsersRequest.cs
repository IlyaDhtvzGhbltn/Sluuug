using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Search
{
    public class SearchUsersRequest
    {
        public string userName { get; set; }

        public int userCountry { get; set; }

        public int userCity { get; set; }

        public int userSex { get; set; }

        public int userAge { get; set; }

        public int userDatingPurpose { get; set; }

        public int userSearchSex { get; set; }

        public int userSearchAge { get; set; }
    }
}