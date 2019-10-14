using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.VkModels
{
    public class OauthParams
    {
        public string uid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo { get; set; }
        public string photo_rec { get; set; }
        public string hash { get; set; }
    }
}