using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Albums
{
    public class AlbumModel
    {
        public Guid AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public string AlbumDescription { get; set; }


        public string AlbumLabelUrl { get; set; }

        public DateTime CreationTime { get; set; }

        public List<FotoModel> Fotos { get; set; }

        public int FotosCount { get; set; }
    }
}