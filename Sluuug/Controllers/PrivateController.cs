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
        //[HttpGet]
        //public ActionResult my()
        //{
        //    var cookies = Request.Cookies.Get("session_id");
        //    if (cookies != null)
        //    {
        //        SessionTypes sessionType = SessionWorker.GetSessionType(cookies.Value);
        //        if (sessionType == SessionTypes.Private)
        //        {
        //            var userInfoModel = UserWorker.GetUserInfo(cookies.Value);
        //            return View(userInfoModel);
        //        }
        //    }
        //    return RedirectToAction("login", "guest");
        //}

        [HttpGet]
        public ActionResult msg()
        {

            return View();
        }

        [HttpPost]
        public ActionResult send(MessageModel message)
        {
            return View();
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