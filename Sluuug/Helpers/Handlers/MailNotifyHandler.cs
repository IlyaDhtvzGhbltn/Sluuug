using NLog;
using Slug.Helpers.BaseController;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
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
            Logger loggerInternal = LogManager.GetLogger("info_logger");
            loggerInternal.Info("start sending activation email to " + email);
            MailAddress from = new MailAddress(email, displayName);
            MailAddress to = new MailAddress(this.recipient);
            MailMessage m = new MailMessage(from, to);
            m.Subject = WebAppSettings.AppSettings[AppSettingsEnum.smtpSubjectConfirmRegister.ToString()];
            string body = this.modifidedActivationHtml(string.Format("{0}/guest/activate?id={1}#menu", domain, this.activate_param));
            m.Body = body;
            AlternateView txtView = new AlternateView(modifidedActivationText(this.activate_param), "text/html");
            m.AlternateViews.Add(txtView);
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
            smtp.Credentials = new NetworkCredential(email, pass);
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(m);
                loggerInternal.Info("end sending email");
            }
            catch (Exception ex)
            {
                loggerInternal.Info(ex);
            }
        }

        public void SendResetPasswordMail()
        {
            Logger loggerInternal = LogManager.GetLogger("info_logger");
            loggerInternal.Info("start sending reset password email to " + email);
            MailAddress from = new MailAddress(email, displayName);
            MailAddress to = new MailAddress(this.recipient);
            MailMessage m = new MailMessage(from, to);
            m.Subject = WebAppSettings.AppSettings[AppSettingsEnum.smtpSubjectResetPassword.ToString()];
            m.Body = this.modifidedResetPasswordHtml(string.Format("{0}/guest/reset?reset_param={1}", domain, this.activate_param));
            AlternateView txtView = new AlternateView(modifidedResetPasswordText(this.activate_param), "text/html");
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
            smtp.Credentials = new NetworkCredential(email, pass);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        private string modifidedActivationHtml(string url)
        {
            string template = File.ReadAllText(HttpContext.Current.Request.MapPath(("~/Resources/html_templates/activatioMail.html")));
            template = template.Replace("#url_to_activation#", url);
            template = template.Replace("#display_url_to_activation#", url);
            template = template.Replace("#button_url_to_activation#", url);
            return template;
        }

        private string modifidedResetPasswordHtml(string url)
        {
            string template = File.ReadAllText(HttpContext.Current.Request.MapPath(("~/Resources/html_templates/resetPasswordMail.html")));
            template = template.Replace("#button_reset_password_url#", url);
            template = template.Replace("#reset_password_url#", url);
            template = template.Replace("#display_reset_password_url#", url);
            return template;
        }

        private string modifidedActivationText(string url)
        {
            string template = File.ReadAllText(HttpContext.Current.Request.MapPath(("~/Resources/text_templates/resetPasswordMail.txt")));
            template = template.Replace("#reset_password_url#", url);
            return template;
        }

        private string modifidedResetPasswordText(string url)
        {
            string template = File.ReadAllText(HttpContext.Current.Request.MapPath(("~/Resources/text_templates/activationMail.txt")));
            template = template.Replace("#url_to_activation#", url);
            return template;
        }
    }
}