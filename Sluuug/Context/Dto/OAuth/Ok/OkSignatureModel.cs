using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.OAuth.Ok
{
    public class OkSignatureModel
    {
        public string AccessToken { get; set; }

        public string ApplicationSecretKey { get; set; }

        public string AppPublicKey { get; set; }

        public string Format { get; set; }

        public string Method { get; set; }
    }
}