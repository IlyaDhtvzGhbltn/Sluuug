﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.ImageEdit
{
    public static class Resize
    {
        public static string ResizedAvatarUri(string uri, ModTypes mode, int height, int weight = 50)
        {
            int startIndex = uri.IndexOf("upload/") + 7;
            string newUri = uri.Insert(startIndex, string.Format("{0},{1}{2},{3}{4},{5},{6}/", mode, "h_", height, "w_", weight, "c_thumb", "g_face"));
            return newUri;
        }

        public static string ResizedFullPhoto(string originalUrl, long originalHeight, long originalWidth, int containerHeight = 505, int containerWidth = 670)
        {
            int startIndex = originalUrl.IndexOf("upload/") + 7;
            double different = 1;
            if (originalHeight > containerHeight)
                different = (double)originalHeight / (double)containerHeight;
            else if (originalWidth > containerWidth)
                different = (double)originalWidth / (double)containerWidth;
            else
                different = 1;
            double newHeight = Math.Round( originalHeight / different);
            double newWidth = Math.Round( originalWidth / different);
            string newUri = originalUrl.Insert(startIndex, string.Format("{0},{1}{2},{3}{4}/", ModTypes.c_scale, "h_", newHeight, "w_", newWidth));

            return newUri;
        }
    }
}