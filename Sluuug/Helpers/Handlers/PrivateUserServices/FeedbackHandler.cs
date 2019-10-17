using Context;
using Slug.Context.Dto.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers.Handlers
{
    public class FeedbackHandler
    {
        public void SaveFeedback(FeedBackRequest request)
        {
            using (var context = new DataBaseContext())
            {
                context.Feedbacks.Add(new Context.Tables.Feedback()
                {
                     BackEmeil = request.Email,
                     CreatedDate = DateTime.UtcNow,
                     Message = request.Message,
                     Subject = request.Subject,
                     UserID = request.UserID
                });
                context.SaveChanges();
            }
        }
    }
}