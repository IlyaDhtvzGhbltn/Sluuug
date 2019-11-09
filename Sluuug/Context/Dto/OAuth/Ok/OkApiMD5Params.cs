using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.OAuth.Ok
{
    public class OkApiMD5Params
    {
        public string SecretKey { get; set; }

        public string Signature { get; set; }
    }
}