using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Messages
{
    public class PartialHubResponse
    {
        public int ToUserID { get; set; }

        public string FromUserName { get; set; }

        public string FromUserSurname { get; set; }

        public string FromUserAvatarUri { get; set; }
    }
}