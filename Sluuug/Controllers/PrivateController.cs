﻿using Context;
using Slug.Context.Attributes;
using Slug.Helpers;
using Slug.Model;
using Slug.Model.Users;
using Slug.Model.VideoConference;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Slug.Context.Dto.UserWorker;
using Newtonsoft.Json;
using Slug.Context.Dto.Search;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Slug.Helpers.BaseController;

namespace Slug.Controllers
{
    [AuthSlug]
    public class PrivateController : SlugController
    {
        [HttpGet]
        public ActionResult index()
        {
            return RedirectToAction("my", "private");
        }

        [HttpGet]
        public ActionResult my()
        {
            var cookies = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
            var userInfoModel = UsersHandler.GetUserInfo(cookies.Value);
            return View(userInfoModel);
        }

        [HttpPost]
        public ActionResult upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                Account account = new Account(
                  WebAppSettings.AppSettings[AppSettingsEnum.cloud.ToString()],
                  WebAppSettings.AppSettings[AppSettingsEnum.apiKey.ToString()],
                  WebAppSettings.AppSettings[AppSettingsEnum.apiSecret.ToString()]);

                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    Folder = "/users/avatars",
                    File = new FileDescription(fileName, upload.InputStream)
                };
                ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                CloudImageUploadResult result = JsonConvert.DeserializeObject<CloudImageUploadResult>(uploadResult.JsonObj.Root.ToString());
                string cookies = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;

                UsersHandler.ChangeAvatarUri(cookies, result.SecureUrl);
            }
            return RedirectToAction("my", "private");
        }

        [HttpGet]
        public ActionResult cnv()
        {
            
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            var user = UsersHandler.GetUserInfo(sessionId);
            var Conversations = base.ConversationHandler.GetPreConversations(user.UserId);
            return View(Conversations);
        }

        [HttpGet]
        public ActionResult msg(Guid id, int page = 1)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            bool verifyConvers = UsersHandler.CheckConversationBySessionId(sessionId, id);
            if (verifyConvers)
            {
                DialogModel dialog = DialogsHandler.GetMessanges(id, page);
                dialog.DialogId = id;
                return View(dialog);
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public ActionResult user(int id)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;

            var friends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(sessionId, id);
            int ownId = UsersHandler.GetUserInfo(sessionId).UserId;
            if (ownId != id)
            {
                if (friends)
                {
                    return RedirectToAction("friend", "private", new { id = id });
                }
                else
                {
                    ForeignUserViewModel model = UsersHandler.GetForeignUserInfo(sessionId, id);
                    return View(model);
                }
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public ActionResult friend(int id)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            int ownId = UsersHandler.GetUserInfo(sessionId).UserId;

            if (ownId != id)
            {
                bool friends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(sessionId, id);
                if (friends)
                {
                    var fUserModel = new FriendlyUserModel();
                    CutUserInfoModel userInfo = UsersHandler.GetUserInfo(id);

                    fUserModel.AvatarPath = userInfo.AvatarUri;
                    fUserModel.DateOfBirth = userInfo.DateBirth;
                    fUserModel.Sity = userInfo.Sity;
                    fUserModel.Name = userInfo.Name;
                    fUserModel.SurName = userInfo.SurName;
                    fUserModel.UserId = userInfo.UserId;

                    return View(fUserModel);
                }
                else
                    return RedirectToAction("my", "private");
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public ActionResult contacts()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            MyFriendsModel model = base.UsersHandler.GetFriendsBySession(sessionId);
            return View(model);
        }

        [HttpGet]
        public ActionResult invite_video_conversation()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            VideoConferenceModel model = base.VideoConferenceHandler.VideoConferenceModel(sessionId);
            Response.Cache.SetExpires(DateTime.Now.AddYears(-1));
            return View(model);
        }

        [HttpGet]
        public ActionResult v_conversation(Guid id)
        {
            bool isActive = base.VideoConferenceHandler.IsConverenceActive(id);
            if (isActive)
                return View();
            else
                return RedirectToAction("invite_video_conversation", "private");
        }

        [HttpGet]
        public ActionResult crypto_cnv()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            CryptoChatModel model = CryptoChatHandler.GetCryptoChat(sessionId);
            return View(model);
        }

        [HttpGet]
        public ActionResult c_msg(string id, int page = 1)
        {
            var model = CryptoChatHandler.GetCryptoDialogs(id, page);
            if (model != null)
            {
                return View(model);
            }
            else return RedirectToAction("my","private");
        }

        [HttpGet]
        public ActionResult logout()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            SessionTypes type = SessionHandler.GetSessionType(sessionId);
            if (type == SessionTypes.Private)
            {
                SessionHandler.CloseSession(sessionId);
            }
            return RedirectToAction("index", "guest");
        }

        [HttpGet]
        public ActionResult settings()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            UserSettingsModel model = UsersHandler.GetSettings(sessionId);
            return View(model);
        }

        [HttpGet]
        public ActionResult search()
        {
            return View();
        }

        [HttpGet]
        public ActionResult search_result(string user_name, int user_country, int user_city, int user_sex, int user_age, int page = 1)
        {
            var parseRequest = new SearchUsersRequest()
            {
                 userSearchAge = (AgeEnum)user_age,
                 userSearchCountry = user_country,
                 userSearchName = user_name,
                 userSearchSity = user_city,
                 userSearchSex = (SexEnum)user_sex
            };
            int ownID = this.UsersHandler.GetUserInfo(Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value).UserId;
            var response = SearchHandler.SearchUsers(parseRequest, ownID, page);
            foreach (var item in response.Users)
            {
                TimeSpan date = TimeSpan.FromTicks(DateTime.Now.Ticks - item.DateBirth.Ticks);
                item.FullAges = new DateTime(date.Ticks).Year;
            }
            return View(response);
        }
    }
}