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
            m.Body = "<h2>Confirm registration</h2>" +
                "<a  href="+ domain+ "/guest/activate?id=" + this.activate_param + "#menu" + "> clic here</a>";
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

    }
}