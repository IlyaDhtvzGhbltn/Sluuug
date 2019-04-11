using Context;
using Slug.Context.Attributes;
using Slug.Helpers;
using Slug.Model;
using Slug.Model.Users;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        [HttpGet]
        public ActionResult cnv()
        {

            string sessionId = Request.Cookies.Get("session_id").Value;
            var user = UserWorker.GetUserInfo(sessionId);
            var Convers = base.ConverWorker.GetPreConversations(user.UserId);
            return View(Convers);
        }

        [HttpGet]
        public ActionResult msg(int id)
        {

            string sessionId = Request.Cookies.Get("session_id").Value;
            bool verifyConvers = UserWorker.CheckConversationBySessionId(sessionId, id);
            if (verifyConvers)
            {
                DialogModel dialog = DialogWorker.GetLast100msgs(id);
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
            var model = base.UserWorker.GetFriendsBySession(sessionId);
            return View(model);
        }

        [HttpGet]
        public ActionResult video_chat()
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            var model = base.UserWorker.GetFriendsBySession(sessionId);
            Response.Cache.SetExpires(DateTime.Now.AddYears(-1));
            return View(model);
        }

        [HttpGet]
        public ActionResult crypto_cnv()
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            CryptoChatModel model = CryptoChatWorker.GetCryptoChat(sessionId);
            return View(model);
        }

        [HttpGet]
        public ActionResult c_msg(string id)
        {
            var model = CryptoChatWorker.GetCryptoDialogs(id);
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