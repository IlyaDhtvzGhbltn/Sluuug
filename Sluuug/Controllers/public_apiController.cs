using Context;
using Slug.Context;
using Slug.Context.Dto.FeedBack;
using Slug.Helpers;
using Slug.Helpers.BaseController;
using Slug.Helpers.Handlers;
using Slug.Helpers.Handlers.OAuthHandlers;
using Slug.Model.VkModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;


namespace Slug.Controllers
{
    public class public_apiController : SlugController
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
                }
            }
            return true;
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

        [HttpPost]
        public JsonResult feed_back(FeedBackRequest request)
        {
            var ValidateErrors = new List<int>();
            if (string.IsNullOrWhiteSpace(request.Message) || request.Message.Length <= 10 || request.Message.Length > 1000)
            {
                ValidateErrors.Add(FeedBackResponce.Errors.ErrorCodes[FeedBackResponce.Errors.FEEDBACK_MESSAGE_INVALID]);
            }
            if ((int)request.Subject < 0 || (int)request.Subject > 3)
            {
                ValidateErrors.Add(FeedBackResponce.Errors.ErrorCodes[FeedBackResponce.Errors.SUBJECT_INVALID]);
            }

            //string sessionID = GetCookiesValue(this.Request);
            //var user = UsersHandler.GetUserSettings(sessionID);
      
            if (ValidateErrors.Count == 0)
            {
                var handler = new FeedbackHandler();
                handler.SaveFeedback(request);
            }

            return new JsonResult()
            {
                Data = new FeedBackResponce()
                {
                    IsSuccess = true
                }
            };
        }

        [HttpPost]
        public JsonResult is_vkuser_registered(string code)
        {
            var vkHandler = new VkOAuthHandler();
            AccessTokenModel model = vkHandler.GetVkAccessToken(code).GetAwaiter().GetResult();
            if (model == null)
            {
                return new JsonResult() { Data = new VkOauthExistModel { status = vkOAuthStatusEnum.error }  };
            }
            int localUserId =  UsersHandler.VkUserRegistredId(model.UserId);
            if (localUserId > 0)
            {
                string session_id = SessionHandler.OpenSession(SessionTypes.Private, localUserId);
                var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
                cookie.Value = session_id;
                Response.Cookies.Set(cookie);
                return new JsonResult() { Data = new VkOauthExistModel { status = vkOAuthStatusEnum.userExistLocaly } };
            }
            return new JsonResult() { Data = new VkOauthExistModel { status = vkOAuthStatusEnum.userNotExist, vkuid = model.UserId } };
        }
    }
}