using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.FeedBack
{
    public class FeedBackResponce
    {
        public bool IsSuccess { get; set; }
        
        public List<int> ErrorCodes { get; set; }

        public string Reason { get; set; }

        public static class Errors
        {
            public static readonly IDictionary<string, int> ErrorCodes = new Dictionary<string, int>()
            {
                { EMAIL_INVALID, 601 },
                { SUBJECT_INVALID, 602 },
                { FEEDBACK_MESSAGE_INVALID, 603 },
            };

            public const string EMAIL_INVALID = "Return email Address is invalid.";
            public const string SUBJECT_INVALID = "Subject is invalid.";
            public const string FEEDBACK_MESSAGE_INVALID = "Feedback message too short or too long.";
        }
    }
}