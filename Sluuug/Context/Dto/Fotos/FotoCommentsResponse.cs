using Slug.Context.Tables;
using Slug.Model.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Fotos
{
    public class FotoCommentsResponse
    {
        public bool isSuccess { get; set; }

        public string Comment { get; set; }

        public List<FotoCommentModel> FotoComments { get; set; } 

        public string PhotoTitle { get; set; }

        public Guid PhotoID { get; set; }

        public string PhotoDownloadLink { get; set; }

        public string PhotoDescription { get; set; }

        public static class Errors
        {
            public const string NOT_ACCESS = "You don't have an access to current foto.";
            public const string NOT_EXIST = "Foto is not available : deleted or not created.";
            public const string EMPTY_VALUE = "New value is empty.";

        }
    }
}