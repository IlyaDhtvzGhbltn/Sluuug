using Context;
using Slug.Helpers;
using Slug.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Slug.Controllers
{
    //[Authorize()]
    public class PrivateController : SlugController
    {
        [HttpGet]
        public ActionResult my()
        {
            var cookies = Request.Cookies.Get("session_id");
            if (cookies != null)
            {
                SessionTypes sessionType = SessionWorker.GetSessionType(cookies.Value);
                if (sessionType == SessionTypes.Private)
                {
                    var userInfoModel = UserWorker.GetUserInfo(cookies.Value);
                    return View(userInfoModel);
                }
            }
            return RedirectToAction("login", "guest");
        }

        [HttpGet]
        public ActionResult cnv()
        {
            if (Request.Cookies.Get("session_id") != null)
            {
                string sessionId = Request.Cookies.Get("session_id").Value;
                var user = UserWorker.GetUserInfo(sessionId);
                var Convers = base.ConverWorker.GetPreConversations(user.UserId);
                return View(Convers);
            }
            return RedirectToAction("login", "guest");
        }

        [HttpGet]
        public ActionResult msg(int id)
        {
            var sessionId = Request.Cookies.Get("session_id");
            if (sessionId != null)
            {
                string sess_id = sessionId.Value;

                if (!string.IsNullOrWhiteSpace(sess_id))
                {
                    bool verifyConvers = UserWorker.CheckConversationBySessionId(sess_id, id);
                    if (verifyConvers)
                    {
                        var dialog = DialogWorker.GetLast100msgs(id);
                        return View(dialog);
                    }
                }
            }
            return RedirectToAction("my","private");
        }

        [HttpGet]
        public ActionResult logout()
        {
            var cookies = Request.Cookies.Get("session_id");
            if (cookies != null)
            {
                if (!string.IsNullOrWhiteSpace(cookies.Value))
                {
                    SessionTypes type = SessionWorker.GetSessionType(cookies.Value);
                    if (type == SessionTypes.Private)
                    {
                        SessionWorker.CloseSession(cookies.Value);
                    }
                }
            }
            return RedirectToAction("index", "guest");
        }
    }
}