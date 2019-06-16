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

        public bool IsParamActive(string resetParam)
        {
            using (var context = new DataBaseContext())
            {
                var resetRequest = context.ResetPasswords
                    .FirstOrDefault(x => x.ResetParameter == resetParam && x.IsExpired == false);
                if (resetRequest != null)
                    return true;
            }
            return false;
        }

        public int GetUserIDByResetParam(string resetParam)
        {
            using (var context = new DataBaseContext())
            {
                ResetPassword request = context.ResetPasswords.FirstOrDefault(x => x.ResetParameter == resetParam && x.IsExpired == false);
                if (request != null)
                {
                    int userID = request.UserRequestedId;
                    User user = context.Users.FirstOrDefault(x=>x.Id == userID);
                    if (user != null)
                    {
                        return user.Id;
                    }
                }
            }
            return 0;
        }

        public int CompleteRequest(string passHash, string reseParam)
        {
            int userId = 0;
            int userID = GetUserIDByResetParam(reseParam);
            if (userID != 0)
            {
                using (var context = new DataBaseContext())
                {
                    User user = context.Users.FirstOrDefault(x => x.Id == userID);
                    if (user != null)
                    {
                        string hash = Converting.ConvertStringToSHA512(passHash);
                        user.Settings.PasswordHash = hash;
                        context.SaveChanges();
                        userId = user.Id;
                    }
                }
            }
            return userId;
        }

        public void ClosePrevievRequests(int userID)
        {
            using (var context = new DataBaseContext())
            {
                List<ResetPassword> previevRequests = context.ResetPasswords
                    .Where(x => x.UserRequestedId == userID).ToList();
                previevRequests.ForEach(x =>
                {
                    x.IsExpired = true;
                });

                List<Session> sessions = context.Sessions
                    .Where(x => x.UserId == userID && x.Type == (int)SessionTypes.Private).ToList();
                sessions.ForEach(x => 
                {
                    x.Type = (int)SessionTypes.Exit;
                });

                List<UserConnections> connections = context.UserConnections
                    .Where(x => x.UserID == userID && x.ConnectionActiveStatus == true).ToList();
                connections.ForEach(x => 
                {
                    x.ConnectionActiveStatus = false;
                    x.ConnectionOff = DateTime.UtcNow;
                });
                context.SaveChanges();
            }
        }
    }
}