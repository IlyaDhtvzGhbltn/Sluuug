using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.FeedBack
{
    public class FeedBackRequest
    {
        public FeedbackSubjectEnums Subject { get; set; }
        public string Email { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
    }
}