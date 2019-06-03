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

        [HttpGet]
        public ActionResult register()
        {
            HttpCookie session_id = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
            if (session_id == null || string.IsNullOrWhiteSpace(session_id.Value))
                return View();
            else
            {
                //SessionTypes type = SessionHandler.GetSessionType(session_id.Value);
                //if (type == SessionTypes.AwaitEmailConfirm)
                //    return RedirectToAction("login", "guest");
                //else
                    return View();
            }
        }

        [HttpPost]
        public ActionResult userconfirmation(RegisteringUserModel user)
        {
            if (isUserEmpty(user))
            {
                return RedirectToAction("register", "guest");
            }
            else
            {
                UserConfirmationDitails userConfirmation = UsersHandler.RegisterNew(user);
                if (userConfirmation != null)
                {
                    var mailer = new MailNotifyHandler(user.Email, userConfirmation.ActivatioMailParam);
                    mailer.SendActivationMail();
                    return RedirectToAction("login", "guest", new
                    { a = userConfirmation.ActivationSessionId });
                }
                else
                    return RedirectToAction("user_already_exist", "error");
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

        [HttpGet]
        public ActionResult login(string a)
        {
            ViewBag.await_confirmation_div_display = "none";
            ViewBag.immediately_login_div_display = "block";

            //var cook = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
            //if (cook != null)
            //{
            //    SessionTypes sessionType = SessionWorker.GetSessionType(cook.Value);
            //    if (sessionType == SessionTypes.Private)
            //    {
            //        return RedirectToAction("my", "private");
            //    }
            //    if (sessionType == SessionTypes.AwaitEmailConfirm)
            //    {
            //        ViewBag.await_confirmation_div_display = "block";
            //        ViewBag.immediately_login_div_display = "none";
            //    }
            //}

            if (!string.IsNullOrWhiteSpace(a))
            {
                SessionTypes type = SessionHandler.GetSessionType(a);
                if (type == SessionTypes.AwaitEmailConfirm)
                {
                    ViewBag.await_confirmation_div_display = "block";
                    ViewBag.immediately_login_div_display = "none";


                    var cookies = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
                    cookies.Value = a;
                    Response.Cookies.Set(cookies);
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
               string.IsNullOrWhiteSpace(user.ForName) &&
               string.IsNullOrWhiteSpace(user.Name) &&
               string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                return true;
            }
            return false;
        }

    }
}