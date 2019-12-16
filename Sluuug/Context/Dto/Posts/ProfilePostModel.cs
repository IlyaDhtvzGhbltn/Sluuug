using Slug.Model.FullInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Posts
{
    public class ProfilePostModel
    {
        public List<PostModel> Posts { get; set; }
        public int TotalPostsCount { get; set; }
    }
}