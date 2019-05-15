using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.ImageEdit
{
    public static class Resize
    {
        public static string ResizedUri(string uri, ModTypes mode, int height)
        {
            int startIndex = uri.IndexOf("upload/") + 7;
            string newUri = uri.Insert(startIndex, string.Format("{0},{1}{2}/", mode, "h_", height));
            return newUri;
        }
    }
}