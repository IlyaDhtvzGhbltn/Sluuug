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
using NLog;
using System.Threading.Tasks;
using System.Threading;
using Slug.Model.Registration;

namespace Slug.Controllers
{
    [SessionCheck]
    public class guestController : SlugController
    {
        [HttpGet]
        public async Task<ActionResult> index()
        {
            await saveClientIpAsync(this.Request);
            int referalUserId = 0;
            if (int.TryParse(Request.Params.Get("ref"), out referalUserId))
            {
                Response.Cookies.Set(new HttpCookie("ref")
                {
                    Value = referalUserId.ToString()
                });
            };

            ViewBag.Title = "FRIENDLYNET - социальная сеть для знакомств и общения с видео-связью и end-to-end шифрованием";
            ViewBag.Description = "FRIENDLYNET - это современный бесплатный сервис для поиска знакомств. Видео-связь в высоком разрешении. Шифрование сообщений end-to-end.";
            ViewBag.MinRegistrationDate = new DateTime(1900, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.MaxRegistrationDate = DateTime.Now.AddYears(-14).ToString("yyyy-MM-dd");

            ViewBag.IsIndex = true;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> activate(string id)
        {
            ViewBag.Title = "FRIENDLYNET | Активация учетной записи";
            ViewBag.Description = "FRIENDLYNET | Активация учетной записи";
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
                        HttpCookie refaralUserIdCookie = Request.Cookies.Get("ref");
                        int referalUserId = 0;
                        if (refaralUserIdCookie != null && !string.IsNullOrWhiteSpace(refaralUserIdCookie.Value))
                        {
                            if (int.TryParse(refaralUserIdCookie.Value, out referalUserId))
                            {
                                await UsersHandler.SetReferal(referalUserId, user.UserId);
                            }
                        }

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
            ViewBag.Title = "FRIENDLYNET | Восстановление учетной записи";
            ViewBag.Description = "FRIENDLYNET | Восстановление учетной записи";
            ViewBag.IsIndex = false;
            ResetPasswordHandler resetHandler = new ResetPasswordHandler();
            ViewBag.IsActive = resetHandler.IsParamActive(reset_param);
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> userconfirmation(RegisteringUserModel user)
        {
            if (isUserEmpty(user))
            {
                return new JsonResult() { Data = false };
            }
            else
            {
                UserConfirmationDitails userConfirmation = await UsersHandler.RegisterNew(user);
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

        [HttpGet]
        public ActionResult vk_oauth(string code)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ok_oauth(string code)
        {
            return View();
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

        private async Task saveClientIpAsync (HttpRequestBase request)
        {
            await Task.Run(()=> 
            {
                try
                {
                    Logger logger = LogManager.GetLogger("info_logger");
                    string info = string.Empty;
                    info = string.Format("{1}UserHostName: {0}{1}", request.UserHostName, Environment.NewLine);
                    info += string.Format("UserHostAddress: {0}{1}", request.UserHostAddress, Environment.NewLine);

                    string languages = string.Empty;
                    foreach (var lang in request.UserLanguages)
                    {
                        languages = string.Format("{0}", lang);
                    }
                    info += string.Format("UserLanguages: {0}{1}", languages, Environment.NewLine);

                    info += string.Format("UrlReferrer: {0}{1}", request.UrlReferrer, Environment.NewLine);
                    info += string.Format("UserAgent: {0}{1}{1}", request.UserAgent, Environment.NewLine);
                    logger.Info(info);
                }
                catch (Exception) { }
            });
        }

        //[HttpGet]
        //public void testmailquality(string mail)
        //{
        //    MailNotifyHandler handler = new MailNotifyHandler(mail, "123");
        //    handler.SendActivationMail();
        //}

        //[HttpGet]
        //public void testmailqualityreset(string mail)
        //{
        //    MailNotifyHandler handler = new MailNotifyHandler(mail, "123");
        //    handler.SendResetPasswordMail();
        //}
    }
}