﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

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

        public void SendActivationMail()
        {
            MailAddress from = new MailAddress("alter.22.04@gmail.com", "Tom");
            MailAddress to = new MailAddress(this.recipient);
            MailMessage m = new MailMessage(from, to);
            string domain = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            m.Subject = "Slug Confirm your registration";
            m.Body = "<h2>To confirm registration</h2>" +
                "<a  href="+ domain+"/guest/activate?id=" + this.activate_param + "> clic here</a>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("alter.22.04@gmail.com", "Quipu20041889ss");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}