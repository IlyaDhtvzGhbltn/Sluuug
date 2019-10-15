using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.VkModels
{
    public class VkOauthExistModel
    {
        public vkOAuthStatusEnum status { get; set; }

        public long vkuid { get; set; } 
    }
}