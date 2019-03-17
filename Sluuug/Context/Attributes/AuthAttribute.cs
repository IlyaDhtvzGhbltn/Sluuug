using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Slug.Context.Attributes
{
    using global::Context;

    [AttributeUsage(AttributeTargets.Class)]
    public class AuthSlugAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
            if (request.Cookies!= null)
            {
                var slugCookie = request.Cookies.Get("session_id");
                if(slugCookie != null)
                {
                    if (!string.IsNullOrWhiteSpace(slugCookie.Value))
                    {
                        string sessionId = slugCookie.Value;
                        var sW = new SessionWorker();
                        var type = sW.GetSessionType(sessionId);
                        if (type == SessionTypes.Private)
                        {
                            return;
                        }
                        else
                            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
                    else
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            else
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
    }
}