using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Search
{
    public class SearchMoreUserRequest: SearchUsersRequest
    {
        public int Page { get; set; }
    }
}