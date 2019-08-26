using Slug.Helpers.BaseController;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;


namespace Slug.Context
{
    public class MailNotifyHandler
    {

        private readonly string email = WebAppSettings.AppSettings[AppSettingsEnum.smtpServerEmail.ToString()];
        private readonly string displayName = WebAppSettings.AppSettings[AppSettingsEnum.smtpServerDisplayName.ToString()];
        private readonly string domain = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        private readonly string smtpHost = WebAppSettings.AppSettings[AppSettingsEnum.smtpHost.ToString()];
        private readonly int smtpPort = int.Parse(WebAppSettings.AppSettings[AppSettingsEnum.smtpPort.ToString()]);
        private readonly string pass = WebAppSettings.AppSettings[AppSettingsEnum.smtpServerPassword.ToString()];

        private string recipient { get; }
        private string activate_param { get; }


        public MailNotifyHandler(string recipient, string activate_param)
        {
            this.recipient = recipient;
            this.activate_param = activate_param;
        }

        public MailNotifyHandler(string recipient)
        {
            this.recipient = recipient;
        }

        public void SendActivationMail()
        {

            MailAddress from = new MailAddress(email, displayName);
            MailAddress to = new MailAddress(this.recipient);
            MailMessage m = new MailMessage(from, to);
            
            m.Subject = WebAppSettings.AppSettings[AppSettingsEnum.smtpSubjectConfirmRegister.ToString()];

            string body = this.modifidedHtml(string.Format("{0}/guest/activate?id={1}#menu", domain, this.activate_param));
            m.Body = body;
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
            smtp.Credentials = new NetworkCredential(email, pass);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        public void SendResetPasswordMail()
        {
            MailAddress from = new MailAddress(email, displayName);
            MailAddress to = new MailAddress(this.recipient);
            MailMessage m = new MailMessage(from, to);
            m.Subject = WebAppSettings.AppSettings[AppSettingsEnum.smtpSubjectResetPassword.ToString()];
            m.Body = "<h2>Reset Your Password</h2>" +
                "<a  href=" + domain + "/guest/reset?reset_param=" + this.activate_param + "> clic here</a>";
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
            smtp.Credentials = new NetworkCredential(email, pass);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        private string modifidedHtml(string url)
        {

            string template = File.ReadAllText(HttpContext.Current.Request.MapPath(("~/Resources/html_templates/activatioMail.html")));
            int indexHref = template.IndexOf("##");
            template = template.Insert(indexHref, url);
            int indexABlock = template.IndexOf("a_url_here");
            template = template.Insert(indexABlock, url);
            int indexJS = template.IndexOf("url_here");
            template = template.Insert(indexJS, url);


            return template;
        }
    }
}