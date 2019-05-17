using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Albums
{
    public class DropAlbumResponse
    {
        public bool isSuccess { get; set; }

        public string Comment { get; set; }

        public static class Errors
        {
            public const string NOT_ACCESS = "You don't have an access to current album.";
            public const string NOT_EXIST = "Album is not available : deleted or not created.";
        }
    }
}