using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using Slug.DbInitialisation;
using NLog;
using System.Threading;
using System.Collections.Generic;
using Slug.Helpers;
using Slug.Context.ServerError;
using Slug.Context;

namespace Sluuug
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //NewUserInitial.Initialize(100);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastErrorInfo = Server.GetLastError();
            Exception errorInfo = lastErrorInfo.GetBaseException();
            HttpException error = errorInfo as HttpException;

            bool isNotFound = false;
            if (lastErrorInfo != null)
            {
                Logger loggerInternal = LogManager.GetLogger("internal_error_logger");
                loggerInternal.Error(lastErrorInfo);


                if (error != null)
                {
                    Logger logger = LogManager.GetLogger("http_exception_logger");
                    logger.Trace(error);
                    isNotFound = error.GetHttpCode() == (int)HttpStatusCode.NotFound;
                }
            }
            if (isNotFound)
            {
                Server.ClearError();
                Response.Redirect("~/error/notfound");
            }
            else if (error != null)
            {
                Server.ClearError();
                Response.Redirect("~/error/custom?error=" + error.WebEventCode);
            }
            else
            {
                Server.ClearError();
                Response.Redirect("~/error/ooops");
            }
        }

        private void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }
        

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //MailNotifyHandler h = new MailNotifyHandler("alter.22.04@gmail.com", "1111");
            //h.SendActivationMail();

            //Don't rewrite requests for content (.png, .css) or scripts (.js)
            if (Request.Url.ToString().Contains("Content") ||
                Request.Url.ToString().Contains("Scripts") ||
                Request.Url.ToString().Contains("Sockets") ||
                Request.Url.ToString().Contains("Token") ||
                Request.Url.ToString().Contains("Protocol")
                )
                return;

            // If uppercase chars exist, redirect to a lowercase version
            var url = Request.Url.ToString();
            if (Regex.IsMatch(url, @"[A-Z]"))
            {
                Response.Redirect(url, false);
                Context.ApplicationInstance.CompleteRequest();
            }

            var ip = Slug.Helpers.SlugController.GetIPAddress(base.Request);
            CultureByIpChecker culture = new CultureByIpChecker(ip);
            Thread.CurrentThread.CurrentCulture = culture.GetCulture();
        }
    }
}
