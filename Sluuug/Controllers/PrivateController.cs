using Context;
using Slug.Context.Attributes;
using Slug.Helpers;
using Slug.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Slug.Controllers
{
    [AuthSlug]
    public class PrivateController : SlugController
    {
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