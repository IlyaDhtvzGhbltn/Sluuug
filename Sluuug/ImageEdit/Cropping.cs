using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.ImageEdit
{
    public static class Cropping
    {
        public static string Cropp(string uri, int weigh, int height, string radius)
        {
            int startIndex = uri.IndexOf("upload/") + 7;
            string newUri = uri.Insert(startIndex, string.Format("{0}{1},{2}{3},{4}{5}/", "w_", weigh, "h_", height, "r_", radius));
            return newUri;
        }
    }
}