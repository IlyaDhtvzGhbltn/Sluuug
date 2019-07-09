using Context;
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
using Slug.Context.Dto.Cloudinary;

namespace Slug.Controllers
{
    [AuthSlug]
    public class PrivateController : SlugController
    {
        [HttpGet]
        public async Task<ActionResult> index()
        {
            return RedirectToAction("my", "private");
        }

        [HttpGet]
        public async Task<ActionResult> my()
        {
            var cookies = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
            FullUserInfoModel userInfoModel = UsersHandler.GetFullUserInfo(cookies.Value);
            return View(userInfoModel);
        }

        [HttpPost]
        public async Task<ActionResult> upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                CloudImageUploadResult result = UploadImg(upload);
                string cookies = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
                UsersHandler.ChangeAvatarUri(cookies, result.SecureUrl);
            }
            return RedirectToAction("my", "private");
        }

        [HttpGet]
        public async Task<ActionResult> cnv()
        {
            
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            var user = UsersHandler.GetFullUserInfo(sessionId);
            ConversationsModel Conversations = base.ConversationHandler.GetPreConversations(user.UserId);
            return View(Conversations);
        }

        [HttpGet]
        public async Task<ActionResult> msg(Guid id, int page = 1)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            bool verifyConvers = UsersHandler.CheckConversationBySessionId(sessionId, id);
            if (verifyConvers)
            {
                int userID = UsersHandler.GetFullUserInfo(sessionId).UserId;
                DialogModel dialog = DialogsHandler.GetMessanges(id, userID, page);
                dialog.DialogId = id;
                return View(dialog);
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public async Task<ActionResult> user(int id)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;

            var friends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(sessionId, id);
            int ownId = UsersHandler.GetFullUserInfo(sessionId).UserId;
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
        public async Task<ActionResult> friend(int id)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            int ownId = UsersHandler.GetFullUserInfo(sessionId).UserId;

            if (ownId != id)
            {
                bool friends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(sessionId, id);
                if (friends)
                {
                    FullUserInfoModel userInfo = UsersHandler.GetFullUserInfo(id);
                    return View(userInfo);
                }
                else
                    return RedirectToAction("my", "private");
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public async Task<ActionResult> contacts()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            MyFriendsModel model = base.UsersHandler.GetFriendsBySession(sessionId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> invite_video_conversation()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            VideoConferenceModel model = base.VideoConferenceHandler.VideoConferenceModel(sessionId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> v_conversation(Guid id)
        {
            bool isActive = base.VideoConferenceHandler.IsConverenceActive(id);
            if (isActive)
                return View();
            else
                return RedirectToAction("invite_video_conversation", "private");
        }

        [HttpGet]
        public async Task<ActionResult> crypto_cnv()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            CryptoChatModel model = CryptoChatHandler.GetCryptoChat(sessionId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> c_msg(string id, int page = 1)
        {
            string session = GetCookiesValue(this.Request);
            CryptoDialogModel model = CryptoChatHandler.GetCryptoDialogs(session, id, page);
            if (model != null)
            {
                return View(model);
            }
            else return RedirectToAction("my","private");
        }

        [HttpGet]
        public async Task<ActionResult> logout()
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
        public async Task<ActionResult> settings()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            UserSettingsModel model = UsersHandler.GetSettings(sessionId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> search()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> search_result(string user_name, int user_country, int user_city, int user_sex, int user_age, int page = 1)
        {
            var parseRequest = new SearchUsersRequest()
            {
                 userSearchAge = (AgeEnum)user_age,
                 userSearchCountry = user_country,
                 userSearchName = user_name,
                 userSearchSity = user_city,
                 userSearchSex = (SexEnum)user_sex
            };
            int ownID = this.UsersHandler.GetFullUserInfo(Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value).UserId;
            SearchUsersResponse response = SearchHandler.SearchUsers(parseRequest, ownID, page);
            foreach (var item in response.Users)
            {
                TimeSpan date = TimeSpan.FromTicks(DateTime.Now.Ticks - item.DateBirth.Ticks);
                item.FullAges = new DateTime(date.Ticks).Year;
            }
            return View(response);
        }

        [HttpGet]
        public async Task<ActionResult> support()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> notification_history()
        {
            return View();
        }
    }
}