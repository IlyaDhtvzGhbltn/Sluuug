using Context;
using Slug.Context.Tables;
using Slug.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers.Handlers
{
    public class ResetPasswordHandler
    {
        public string CreateRequest(string email, int userID)
        {
            string parameter = Converting.ConvertStringtoMD5(DateTime.Now.Ticks.ToString());

            var resetPasswordRequest = new ResetPassword();
            resetPasswordRequest.CreateRequestDate = DateTime.Now;
            resetPasswordRequest.UserRequestedId = userID;
            resetPasswordRequest.ResetParameter = parameter;
            resetPasswordRequest.IsExpired = false;
            resetPasswordRequest.Email = email;

            using (var context = new DataBaseContext())
            {
                List<ResetPassword> previevRequests = context.ResetPasswords.Where(x => x.UserRequestedId == userID).ToList();
                previevRequests.ForEach(x => 
                {
                    x.IsExpired = true;
                });
                context.ResetPasswords.Add(resetPasswordRequest);
                context.SaveChanges();
                return parameter;
            }
        }

        public void CompleteRequest()
        {

        }

        public void ClosePrevievRequests(int userID)
        {
            using (var context = new DataBaseContext())
            {
                List<ResetPassword> previevRequests = context.ResetPasswords.Where(x => x.UserRequestedId == userID).ToList();
                previevRequests.ForEach(x =>
                {
                    x.IsExpired = true;
                });
                context.SaveChanges();
            }
        }
    }
}