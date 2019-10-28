using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.OAuth
{
    public class VkToken
    {
        public string Code { get; set; }
        public long VkUserId { get; set; }
        public string Token { get; set; }
    }
}