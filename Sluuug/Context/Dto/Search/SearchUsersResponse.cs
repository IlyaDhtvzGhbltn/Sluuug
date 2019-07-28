﻿using System.Collections.Generic;
using Slug.Model.Users;

namespace Slug.Context.Dto.Search
{
    public class SearchUsersResponse
    {
        public ICollection<BaseUser> Users { get; set; }
        public int PagesCount { get; set; }
    }
}