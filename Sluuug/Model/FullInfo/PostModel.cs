using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.FullInfo
{
    public class PostModel
    {
        public int UserPosterId { get; set; }

        public string PostTitle { get; set; }

        public string PostText { get; set; }

        public string PostedTime { get; set; }
    }
}