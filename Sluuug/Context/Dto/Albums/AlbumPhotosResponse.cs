using Slug.Model.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Albums
{
    public class AlbumPhotosResponse
    {
        public bool Succes { get; set; }

        public string Comment { get; set; }

        public int PhotosCount { get; set; }

        public List<FotoModel> Photos { get; set; }

        public static class Errors
        {
            public const string NOT_ACCESS = "You don't have an access to current album.";
            public const string NOT_EXIST = "Album is not available : deleted or not created.";
        }
    }
}