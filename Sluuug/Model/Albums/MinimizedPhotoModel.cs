using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Albums
{
    public class MinimizedPhotoModel
    {
        public Guid PhotoId { get; set; }

        public string PhotoTitle { get; set; }

        public string PhotoDescription { get; set; }

        public string PhotoSmallUrl { get; set; }

        public string PhotoFullUrl { get; set; }
    }
}