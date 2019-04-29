using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Search
{
    public class SearchUsersRequest
    {
        public string userSearchName { get; set; }

        public int userSearchCountry { get; set; }

        public int userSearchSity { get; set; }

        public SexEnum userSearchSex { get; set; }

        public AgeEnum userSearchAge { get; set; }
    }
}