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

        public SexEnum userSex { get; set; }

        public AgeEnum userAge { get; set; }

        public DatingPurposeEnum userDatingPurpose { get; set; }


        public int userSearchSex { get; set; }

        public int userSearchAge { get; set; }
    }
}