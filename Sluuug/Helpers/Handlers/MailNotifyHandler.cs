using Slug.Helpers.BaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;


namespace Slug.Context
{
    public class MailNotifyHandler
    {
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
            string email = WebAppSettings.AppSettings[AppSettingsEnum.smtpServerEmail.ToString()];
            string displayName = WebAppSettings.AppSettings[AppSettingsEnum.smtpServerDisplayName.ToString()];
            MailAddress from = new MailAddress(email, displayName);
            MailAddress to = new MailAddress(this.recipient);
            MailMessage m = new MailMessage(from, to);
            string domain = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            m.Subject = WebAppSettings.AppSettings[AppSettingsEnum.smtpSubject.ToString()];
            m.Body = "<h2>Confirm registration</h2>" +
                "<a  href="+ domain+ "/guest/activate?id=" + this.activate_param + "#menu" + "> clic here</a>";
            m.IsBodyHtml = true;

            string smtpHost = WebAppSettings.AppSettings[AppSettingsEnum.smtpHost.ToString()];
            int smtpPort = int.Parse(WebAppSettings.AppSettings[AppSettingsEnum.smtpPort.ToString()]);
            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
            string pass = WebAppSettings.AppSettings[AppSettingsEnum.smtpServerPassword.ToString()];
            smtp.Credentials = new NetworkCredential(email, pass);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        public void SendResetPasswordMail()
        {

        }

    }
}