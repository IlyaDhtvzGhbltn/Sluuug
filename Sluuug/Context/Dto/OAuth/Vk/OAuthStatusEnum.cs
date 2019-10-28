using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Dto.OAuth
{
    public enum OAuthStatusEnum
    {
        userNotExist = 0,
        userExistLocaly = 1,
        error = 10
    }
}