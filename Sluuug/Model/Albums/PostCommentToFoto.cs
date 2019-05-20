using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Albums
{
    public class PostCommentToFoto
    {
        [JsonProperty("foto")]
        public Guid FotoID { get; set; }

        [JsonProperty("comment")]
        public string CommentText { get; set; }
    }
}