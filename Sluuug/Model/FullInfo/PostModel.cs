using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.FullInfo
{
    public class PostModel
    {
        public int UserPosterId { get; set; }

        public string Text { get; set; }

        public DateTime PostedTime { get; set; }
    }
}