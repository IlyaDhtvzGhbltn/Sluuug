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
            var cookies = Request.Cookies.Get("session_id");
            var userInfoModel = UserWorker.GetUserInfo(cookies.Value);
            return View(userInfoModel);
        }

        [HttpPost]
        public ActionResult upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                Account account = new Account(
                  "dlk1sqmj4",
                  "846574769361479",
                  "XrQqn5IpPmsnIS3s5PCrvUr-3xw");

                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    Folder = "/users/avatars",
                    File = new FileDescription(fileName, upload.InputStream)
                };
                ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                CloudImageUploadResult result = JsonConvert.DeserializeObject<CloudImageUploadResult>(uploadResult.JsonObj.Root.ToString());
                string cookies = Request.Cookies.Get("session_id").Value;

                UserWorker.ChangeAvatarUri(cookies, result.SecureUrl);
            }
            return RedirectToAction("my", "private");
        }

        [HttpGet]
        public ActionResult cnv()
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            var user = UserWorker.GetUserInfo(sessionId);
            var Conversations = base.ConverWorker.GetPreConversations(user.UserId);
            return View(Conversations);
        }

        [HttpGet]
        public ActionResult msg(Guid id, int page = 1)
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            bool verifyConvers = UserWorker.CheckConversationBySessionId(sessionId, id);
            if (verifyConvers)
            {
                DialogModel dialog = DialogWorker.GetMessanges(id, page);
                dialog.DialogId = id;
                return View(dialog);
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public ActionResult user(int id)
        {
            string sessionId = Request.Cookies.Get("session_id").Value;

            var friends = UserWorker.IsUsersAreFriends(sessionId, id);
            int ownId = UserWorker.GetUserInfo(sessionId).UserId;
            if (ownId != id)
            {
                if (friends)
                {
                    return RedirectToAction("friend", "private", new { id = id });
                }
                else
                {
                    var model = new ForeignUserViewModel();
                    var userInfo = UserWorker.GetUserInfo(id);
                    model.AvatarPath = userInfo.AvatarUri;
                    model.Name = userInfo.Name;
                    model.SurName = userInfo.SurName;

                    return View(model);
                }
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public ActionResult friend(int id)
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            int ownId = UserWorker.GetUserInfo(sessionId).UserId;

            if (ownId != id)
            {
                var fUserModel = new FriendlyUserModel();
                var userInfo = UserWorker.GetUserInfo(id);

                fUserModel.AvatarPath = userInfo.AvatarUri;
                fUserModel.DateOfBirth = userInfo.DateBirth;
                fUserModel.Sity = userInfo.Sity;
                fUserModel.MetroStation = userInfo.MetroStation;
                fUserModel.Name = userInfo.Name;
                fUserModel.SurName = userInfo.SurName;
                fUserModel.UserId = userInfo.UserId;

                return View(fUserModel);
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public ActionResult contacts()
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            MyFriendsModel model = base.UserWorker.GetFriendsBySession(sessionId);
            return View(model);
        }

        [HttpGet]
        public ActionResult invite_video_conversation()
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            VideoConferenceModel model = base.VideoConferenceWorker.VideoConferenceModel(sessionId);
            Response.Cache.SetExpires(DateTime.Now.AddYears(-1));
            return View(model);
        }

        [HttpGet]
        public ActionResult v_conversation(Guid id)
        {
            bool isActive = base.VideoConferenceWorker.IsConverenceActive(id);
            if (isActive)
                return View();
            else
                return RedirectToAction("invite_video_conversation", "private");
        }

        [HttpGet]
        public ActionResult crypto_cnv()
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            CryptoChatModel model = CryptoChatWorker.GetCryptoChat(sessionId);
            return View(model);
        }

        [HttpGet]
        public ActionResult c_msg(string id, int page = 1)
        {
            var model = CryptoChatWorker.GetCryptoDialogs(id, page);
            if (model != null)
            {
                return View(model);
            }
            else return RedirectToAction("my","private");
        }

        [HttpGet]
        public ActionResult logout()
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            SessionTypes type = SessionWorker.GetSessionType(sessionId);
            if (type == SessionTypes.Private)
            {
                SessionWorker.CloseSession(sessionId);
            }
            return RedirectToAction("index", "guest");
        }
    }
}