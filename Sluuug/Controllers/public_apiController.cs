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
        public void resetpassword(string email)
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
                }
            }
        }

        [HttpPost]
        public void reset_password_confirm(string passHash, string reset_param)
        {

        }
    }
}