using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Albums
{
    public class UploadModel
    {
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}