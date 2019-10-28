using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.OAuth
{
    public class FBUserInfo
    {
        public long id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string birthday { get; set; }
        public string gender { get; set; }
        public Hometown hometown { get; set; }
        public Location location { get; set; }
        public Picture picture { get; set; }
    }

    public class Hometown
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Location
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Data
    {
        public int height { get; set; }
        public bool is_silhouette { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Picture
    {
        public Data data { get; set; }
    }
}