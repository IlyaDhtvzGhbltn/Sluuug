using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace Sluuug
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastErrorInfo = Server.GetLastError();
            Exception errorInfo = null;

            bool isNotFound = false;
            bool internalServerError = false;
            if (lastErrorInfo != null)
            {
                errorInfo = lastErrorInfo.GetBaseException();
                var error = errorInfo as HttpException;
                if (error != null)
                {
                    isNotFound = error.GetHttpCode() == (int)HttpStatusCode.NotFound;
                }
                if (errorInfo.Message.Contains("contains a null entry for parameter"))
                {
                    internalServerError = true;
                }
            }
            if (isNotFound)
            {
                Server.ClearError();
                Response.Redirect("~/error/notfound");
            }

            if (internalServerError)
            {
                Server.ClearError();
                Response.Redirect("~/guest/index");
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Don't rewrite requests for content (.png, .css) or scripts (.js)
            if (Request.Url.ToString().Contains("Content") ||
                Request.Url.ToString().Contains("Scripts") ||
                Request.Url.ToString().Contains("Sockets")
                )
                return;

            // If uppercase chars exist, redirect to a lowercase version
            var url = Request.Url.ToString();
            if (Regex.IsMatch(url, @"[A-Z]"))
            {
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
                Response.AddHeader("Location", url.ToLower());
                Response.End();
            }


            var session_id = Request.Cookies.Get("session_id");
            if (session_id != null)
            {

            }
        }
        //protected void Application_EndRequest()
        //{
        //    if (Context.Response.StatusCode == 404)
        //    {
        //        Response.Clear();

        //        var rd = new RouteData();
        //        rd.DataTokens["area"] = "AreaName"; // In case controller is in another area
        //        rd.Values["controller"] = "Errors";
        //        rd.Values["action"] = "NotFound";

        //        Response.Redirect("~/err/notfound");
        //    }
        //}

    }
}
