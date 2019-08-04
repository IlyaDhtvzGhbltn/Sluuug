using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Albums
{
    public class CreateAlbumResponse
    {
        public bool isSuccess { get; set; }

        public string Comment { get; set; }

        public Guid AlbumId { get; set; }
    }
}