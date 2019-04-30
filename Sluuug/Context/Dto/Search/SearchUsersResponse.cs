using Slug.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Search
{
    public class SearchUsersResponse
    {
        public ICollection<CutUserInfoModel> Users { get; set; }
        public int TotalCount { get; set; }
    }
}