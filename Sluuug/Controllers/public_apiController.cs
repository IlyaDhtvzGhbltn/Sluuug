using Context;
using NLog;
using Slug.Context;
using Slug.Context.Dto.FeedBack;
using Slug.Context.Dto.OAuth;
using Slug.Context.Dto.OAuth.Ok;
using Slug.Crypto;
using Slug.Dto.OAuth;
using Slug.Helpers;
using Slug.Helpers.BaseController;
using Slug.Helpers.Handlers;
using Slug.Helpers.Handlers.OAuthHandlers;
using Slug.Model;
using Slug.Model.VkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<JsonResult> is_vkuser_registered(string code)
        {
            var vkHandler = new VkOAuthHandler();
            AccessTokenModel model = vkHandler.GetAccessToken(code).GetAwaiter().GetResult();
            if (model == null)
            {
                return new JsonResult() { Data = new OauthExistStatus { status = OAuthStatusEnum.error }  };
            }
            var hand = new OauthHandler();
            int localUserId = hand.VkUserRegistredId(model.VkUserId);
            if (localUserId > 0)
            {
                string session_id = SessionHandler.OpenSession(SessionTypes.Private, localUserId);
                var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
                cookie.Value = session_id;
                Response.Cookies.Set(cookie);
                return new JsonResult() { Data = new OauthExistStatus { status = OAuthStatusEnum.userExistLocaly } };
            }
            else
            {
                var handler = new OauthHandler();
                await handler.SaveTokenEntry(new VkToken() { Code = code, VkUserId = model.VkUserId, Token = model.AccessToken });
                return new JsonResult() { Data = new OauthExistStatus { status = OAuthStatusEnum.userNotExist } };
            }
        }

        [HttpPost]
        public async Task<bool> register_new_vk(string code)
        {
            var vkHandler = new VkOAuthHandler();
            OutRegisteringUserModel userVkInfo = await vkHandler.GetVkUserInfo(code);
            userVkInfo.ReferalUserId = getReferalUserId();
            var registeredUserId = UsersHandler.RegisterNewFromOutNetwork(userVkInfo, "vk", RegisterTypeEnum.VkUser);

            string session_id = SessionHandler.OpenSession(SessionTypes.Private, registeredUserId);
            var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
            cookie.Value = session_id;
            Response.Cookies.Set(cookie);
            return true;
        }

        [HttpPost]
        public async Task<JsonResult> register_new_ok(string code)
        {
            var ok = new OkOauthHandler();
            OkAccessToken token = ok.AccessToken(code).GetAwaiter().GetResult();
            OkUserInfo okUserInfo = ok.UserInfo(token).GetAwaiter().GetResult();
            if(okUserInfo == null)
                return new JsonResult() { Data = new OauthExistStatus { status = OAuthStatusEnum.error } };

            var hand = new OauthHandler();
            long okId = long.Parse(okUserInfo.Uid);
            int localUserId = hand.OkUserRegisteredId(okId);
            if (localUserId > 0)
            {
                string session_id = SessionHandler.OpenSession(SessionTypes.Private, localUserId);
                var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
                cookie.Value = session_id;
                Response.Cookies.Set(cookie);
                return new JsonResult() { Data = new OauthExistStatus { status = OAuthStatusEnum.userExistLocaly } };
            }
            else
            {
                int sex = -1;
                DateTime userDateOfBirth;
                int Country = -1;
                int City = -1;

                if (okUserInfo.Gender == "male")
                    sex = 1;
                else
                    sex = 0;

                if (okUserInfo.BirthdaySet)
                    userDateOfBirth = DateTime.ParseExact(okUserInfo.Birthday, "yyyy-MM-dd", null);
                else
                    userDateOfBirth = DateTime.Now.AddYears(-18);

                if (string.IsNullOrWhiteSpace(okUserInfo.Location.City) || string.IsNullOrWhiteSpace(okUserInfo.Location.Country))
                {
                    Country = 7;
                    City = 495;
                }
                else
                {
                    Country = hand.CountryCodeParse(okUserInfo.Location.CountryName);
                    City = hand.CityCodeParse(okUserInfo.Location.City, Country);
                }

                var model = new OutRegisteringUserModel()
                {
                    Name = okUserInfo.FirstName,
                    SurName = okUserInfo.LastName,
                    Avatar50 = okUserInfo.SmallAvatar,
                    Avatar100 = okUserInfo.MediumAvatar,
                    Avatar200 = okUserInfo.LargeAvatar,
                    OutId = okId,
                    Sex = sex,
                    DateBirth = userDateOfBirth,
                    Status = string.Empty,
                    CityCode = City,
                    CountryCode = Country,
                    ReferalUserId = getReferalUserId()
                };
                int localUserid = UsersHandler.RegisterNewFromOutNetwork(model, "ok", RegisterTypeEnum.OkUser);

                string session_id = SessionHandler.OpenSession(SessionTypes.Private, localUserid);
                var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
                cookie.Value = session_id;
                Response.Cookies.Set(cookie);
                return new JsonResult() { Data = new OauthExistStatus { status = OAuthStatusEnum.userExistLocaly } };
            }
        }

        private int? getReferalUserId()
        {
            HttpCookie refCookie = Request.Cookies.Get("ref");
            if (refCookie != null && !string.IsNullOrWhiteSpace(refCookie.Value))
            {
                int refUserId = 0;
                if (int.TryParse(refCookie.Value, out refUserId))
                {
                    return refUserId;
                }
            }
            return null;
        }

        //[HttpPost]
        //public async Task<JsonResult> verifyFBUser(string code)
        //{
        //    var handler = new FbOAuthHandler();
        //    var preToken = await handler.GetAccessToken(code);
        //    if (preToken == null)
        //        return new JsonResult() { Data = new OauthExistStatus() { status = OAuthStatusEnum.error } };
        //    else
        //    {
        //        FBUserInfo fbUserInfo = await handler.GetUserInfo(preToken.access_token);
        //        var oauthHand = new OauthHandler();
        //        int localUserId = oauthHand.FBUserRegisterId(fbUserInfo.id);
        //        if (localUserId > 0)
        //        {
        //            string session_id = SessionHandler.OpenSession(SessionTypes.Private, localUserId);
        //            var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
        //            cookie.Value = session_id;
        //            Response.Cookies.Set(cookie);
        //            return new JsonResult() { Data = new OauthExistStatus() { status = OAuthStatusEnum.userExistLocaly } };
        //        }
        //        else
        //        {
        //            OutRegisteringUserModel localUser = handler.Convert(fbUserInfo);
        //            int localRegisteredUserId = UsersHandler.RegisterNewFromOutNetwork(localUser, "fb", RegisterTypeEnum.FbUser);
        //            if (localRegisteredUserId > 0)
        //            {
        //                string session_id = SessionHandler.OpenSession(SessionTypes.Private, localRegisteredUserId);
        //                var cookie = new HttpCookie(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
        //                cookie.Value = session_id;
        //                Response.Cookies.Set(cookie);
        //                return new JsonResult() { Data = new OauthExistStatus() { status = OAuthStatusEnum.userNotExist } };
        //            }
        //            else
        //                return new JsonResult() { Data = new OauthExistStatus() { status = OAuthStatusEnum.error } };
        //        }
        //    }
        //}
    }
}