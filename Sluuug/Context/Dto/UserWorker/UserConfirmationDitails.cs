using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.UserWorker
{
    public class UserConfirmationDitails
    {
        public string ActivationSessionId { get; set; }
        public string ActivatioMailParam { get; set; }
    }
}