using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;


namespace Slug.Context.Attributes
{
    using global::Context;
    using Slug.Helpers.BaseController;
    using System.Web.Mvc;
    using System.Web.Routing;

    [AttributeUsage(AttributeTargets.Class)]
    public class SessionCheckAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
            if (request.Cookies != null)
            {
                var slugCookie = request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
                if (slugCookie != null)
                {
                    if (!string.IsNullOrWhiteSpace(slugCookie.Value))
                    {
                        string sessionId = slugCookie.Value;
                        var sW = new SessionsHandler();
                        var type = sW.GetSessionType(sessionId);
                        if (type == SessionTypes.Private)
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "private", action = "my" }));
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
                else
                    return;
            }
            else
                return;
        }
    }
}