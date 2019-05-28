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

namespace Sluuug
{
    public class MvcApplication : HttpApplication
    {
        private readonly Dictionary<string, string> CultureDictionary 
            = new Dictionary<string, string>()
            {
                { "RU", "ru-RU" },
                { "EN", "en-US"},
                { "US", "en-US" }
            };


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //NewUserInitial.UserFullInfo("login18");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastErrorInfo = Server.GetLastError();
            Exception errorInfo = null;

            bool isNotFound = false;
            bool internalServerError = false;
            if (lastErrorInfo != null)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Trace(lastErrorInfo);

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
            else if (internalServerError)
            {
                Server.ClearError();
                Response.Redirect("~/guest/index");
            }
            else
            {
                Response.Redirect("~/error/ooops");
            }
        }

        private void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log as unhandled exception: e.ExceptionObject.ToString()
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

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

            Regex regex = new Regex("[A-Z]{2,}");
            var ip = Slug.Helpers.SlugController.GetIPAddress(base.Request);
            WebClient client = new WebClient();
            string codeUrl = string.Format("https://ipinfo.io/{0}/country", ip);
            try
            {
                string ipResp = client.DownloadString(codeUrl);
                MatchCollection country = regex.Matches(ipResp);
                if (country.Count == 1)
                {
                    string code = country[0].Value;
                    var ruCulture = new System.Globalization.CultureInfo(CultureDictionary[code]);
                    Thread.CurrentThread.CurrentCulture = ruCulture;
                }
                else
                {
                    var enCulture = new System.Globalization.CultureInfo("ru-RU");
                    Thread.CurrentThread.CurrentCulture = enCulture;
                }
            }
            catch (Exception ex)
            {
                var enCulture = new System.Globalization.CultureInfo("ru-RU");
                Thread.CurrentThread.CurrentCulture = enCulture;
            }
        }
    }
}
