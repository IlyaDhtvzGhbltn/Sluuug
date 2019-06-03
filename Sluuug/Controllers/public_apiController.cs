using Context;
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
    }
}