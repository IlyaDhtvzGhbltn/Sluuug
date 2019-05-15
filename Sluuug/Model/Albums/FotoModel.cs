using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Albums
{
    public class FotoModel
    {
        public string FotoUri { get; set; }

        public DateTime UploadDate { get; set; }

        public string Title { get; set; }

        public string AuthorComment { get; set; }

        public Guid Album { get; set; }

        public List<FotoCommentModel> FotoComments { get; set; }
    }
}