using Slug.Context.Attributes;
using Slug.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slug.Controllers
{
    [AuthSlug]
    public class apiController : SlugController
    {

        [HttpPost]
        public JsonResult get_user_info()
        {
            string sessionId = Request.Cookies.Get("session_id").Value;
            var UserInfo = UserWorker.GetUserInfo(sessionId);
            var result = new JsonResult();
            result.Data = UserInfo;
            return result;
        }
    }
}