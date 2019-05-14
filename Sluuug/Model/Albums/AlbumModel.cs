using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Albums
{
    public class AlbumModel
    {
        public Guid Guid { get; set; }

        public string Title { get; set; }

        public string AlbumLabelUrl { get; set; }

        public DateTime CreationTime { get; set; }

        public string AuthorComment { get; set; }

        public List<FotoModel> Fotos { get; set; }
    }
}