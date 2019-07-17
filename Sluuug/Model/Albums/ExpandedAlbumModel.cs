using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Albums
{
    public class ExpandedAlbumModel
    {
        public Guid AlbumId { get; set; }

        public string ExpandedPhotoTitle { get; set; }

        public string ExpandedPhotoDescription { get; set; }

        public string ExpandedPhotoFullUrl { get; set; }

        public List<FotoCommentModel> ExpandedPhotoComments { get; set; }

        public List<MinimizedPhotoModel> MinimizedPhotos { get; set; }
    }
}