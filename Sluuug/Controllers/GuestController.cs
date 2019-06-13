using Slug.Context;
using Slug.Model;
using Context;
using System.Web;
using System.Web.Mvc;
using Slug.Context.Tables;
using Slug.Helpers;
using Slug.Context.Attributes;
using Slug.Context.Dto.UserWorker;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Slug.Helpers.BaseController;

namespace Slug.Controllers
{
    [SessionCheck]
    public class guestController : SlugController
    {
       public ActionResult index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult userconfirmation(RegisteringUserModel user)
        {
            if (isUserEmpty(user))
            {
                return new JsonResult() { Data = false };
            }
            else
            {
                UserConfirmationDitails userConfirmation = UsersHandler.RegisterNew(user);
                if (userConfirmation != null)
                {
                    var mailer = new MailNotifyHandler(user.Email, userConfirmation.ActivatioMailParam);
                    mailer.SendActivationMail();
                    return new JsonResult() { Data = true };
                }
                else
                    return new JsonResult() { Data = false };
            }
        }

        [HttpGet]
        public ActionResult activate(string id)
        {
            ViewBag.incorrect_div_display = "block";
            ViewBag.success_div_display = "none";
            if (!string.IsNullOrWhiteSpace(id))
            {
                ActivationLink user = ActivationMailHandler.GetActivateLink(id);
                if (user != null && user.IsExpired != true)
                {
                    ActivationLnkStatus status = ActivationMailHandler.GetLinkStatus(id);
                    if (status == ActivationLnkStatus.Correct)
                    {
                        UsersHandler.ConfirmUser(user.Id);

                        ActivationMailHandler.CloseActivationEntries(user.Id);

                        string sessionNumber = SessionHandler.OpenSession(SessionTypes.Exit, user.Id);
                        var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
                        cookie.Value = sessionNumber;
                        Response.Cookies.Set(cookie);

                        ViewBag.incorrect_div_display = "none";
                        ViewBag.success_div_display = "block";
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult auth(string login, string hashPassword)
        {
            int userId = UsersHandler.VerifyUser(login, hashPassword);
            if (userId > 0)
            {
                string session_id = SessionHandler.OpenSession(SessionTypes.Private, userId);
                var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
                cookie.Value = session_id;
                Response.Cookies.Set(cookie);

                return RedirectToAction("my", "private");
            }
            return RedirectToAction("login", "guest");
        }


        private bool isUserEmpty(RegisteringUserModel user)
        {
            if
              (string.IsNullOrWhiteSpace(user.Email) &&
               string.IsNullOrWhiteSpace(user.SurName) &&
               string.IsNullOrWhiteSpace(user.Name) &&
               string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                return true;
            }
            return false;
        }
    }
}