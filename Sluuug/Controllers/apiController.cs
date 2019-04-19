using Context;
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

        [HttpPost]
        public JsonResult get_info_other_user(int id)
        {
            var UserInfo = UserWorker.GetUserInfo(id);
            var result = new JsonResult();
            result.Data = UserInfo;
            return result;
        }


        [HttpPost]
        public JsonResult user_vc_role(string converenceID)
         {
            var rrrrr = Request;
            string sessionId = Request.Cookies.Get("session_id").Value;
            var VCWorker = new VideoConferenceWorker();
            var role = VCWorker.UserVCType(sessionId, Guid.Parse(converenceID));
            if(role == VideoConverenceCallType.Caller)
                return new JsonResult() { Data = new Role { type = "CALLER" } };
            if(role == VideoConverenceCallType.Calle)
                return new JsonResult() { Data = new Role { type = "CALLE" } };

            return null;
        }

        private class Role
        {
            public string type { get; set; }
        }
    }
}