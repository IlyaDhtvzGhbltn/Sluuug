using Context;
using Slug.Context;
using Slug.Helpers;
using Slug.Helpers.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slug.Controllers
{
    public class public_apiController : Controller
    {
        [HttpPost]
        public JsonResult verify_login(string login)
        {
            bool freeLogin = false;
            using (var context = new DataBaseContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Login == login);
                if (user == null)
                    freeLogin = true;
            }
            return new JsonResult() { Data = freeLogin };
        }

        [HttpPost]
        public JsonResult verify_email(string email)
        {
            bool freeMail = false;
            using (var context = new DataBaseContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Settings.Email == email);
                if (user == null)
                    freeMail = true;
            }
            return new JsonResult() { Data = freeMail };
        }

        [HttpPost]
        public bool resetpassword(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                UsersHandler uHandler = new UsersHandler();
                int isEmailValid = uHandler.IsEmailValid(email);
                if (isEmailValid > 0)
                {
                    ResetPasswordHandler resetHandler = new ResetPasswordHandler();
                    string parameter = resetHandler.CreateRequest(email, isEmailValid);
                    MailNotifyHandler mailHandler = new MailNotifyHandler(email, parameter);
                    mailHandler.SendResetPasswordMail();
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        public JsonResult reset_password_confirm(string passHash, string reset_param)
        {
            if (!string.IsNullOrWhiteSpace(reset_param) && !string.IsNullOrWhiteSpace(passHash))
            {
                ResetPasswordHandler resetHandler = new ResetPasswordHandler();
                bool flag = resetHandler.IsParamActive(reset_param);
                if (flag)
                {
                    int userID = resetHandler.CompleteRequest(passHash, reset_param);
                    if(userID > 0)
                            resetHandler.ClosePrevievRequests(userID);
                    return new JsonResult() { Data = true };
                }
            }
            return new JsonResult() { Data = false };
        }
    }
}