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
using System;
using Slug.Helpers.Handlers;
using Slug.DbInitialisation;

namespace Slug.Controllers
{
    [SessionCheck]
    public class guestController : SlugController
    {
        [HttpGet]
        public ActionResult index()
        {
            ViewBag.Title = "FriendNote - бесплатная социальная сеть с видео-связью и end-to-end";
            ViewBag.Description = "FriendNote - это современный бесплатный сервис для поиска знакомств, сочетающий в себе видео-связь и end-to-end шифрование сообщений.";
            ViewBag.MinRegistrationDate = new DateTime(1900, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.MaxRegistrationDate = DateTime.Now.AddYears(-14).ToString("yyyy-MM-dd");

            ViewBag.IsIndex = true;
            return View();
        }

        [HttpGet]
        public ActionResult activate(string id)
        {
            ViewBag.Title = "FriendNote | Активация учетной записи";
            ViewBag.Description = "FriendNote | Активация учетной записи";
            ViewBag.IsIndex = false;

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
                        UsersHandler.ConfirmUser(user.UserId);

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
        public ActionResult reset(string reset_param)
        {
            ViewBag.Title = "FriendNote | Восстановление учетной записи";
            ViewBag.Description = "FriendNote | Восстановление учетной записи";
            ViewBag.IsIndex = false;
            ResetPasswordHandler resetHandler = new ResetPasswordHandler();
            ViewBag.IsActive = resetHandler.IsParamActive(reset_param);
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

        [HttpPost]
        public JsonResult auth(LoginingUserModel loginModel)
        {
            int userId = UsersHandler.VerifyUser(loginModel.login, loginModel.hashPassword);
            if (userId > 0)
            {
                string session_id = SessionHandler.OpenSession(SessionTypes.Private, userId);
                var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
                cookie.Value = session_id;
                Response.Cookies.Set(cookie);
                return new JsonResult() { Data = true };
            }
            return new JsonResult() { Data = false };
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