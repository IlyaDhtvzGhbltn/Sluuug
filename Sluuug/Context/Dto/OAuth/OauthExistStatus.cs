using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Dto.OAuth
{
    public class OauthExistStatus
    {
        public OAuthStatusEnum status { get; set; }

        public long UserId { get; set; }
    }
}