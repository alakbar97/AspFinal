using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace OnlineCv
{
    public static partial class Extension
    {
        static public bool SendMail(this string toMails, string subject, string body)
        {
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject");
            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException("body");
            if (string.IsNullOrEmpty(toMails))
                throw new ArgumentNullException("toMails");

            MailMessage mail = new MailMessage(new MailAddress(ConfigurationManager.AppSettings.Get("fromMail"), "OnlineCv"),
                new MailAddress(toMails.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0]))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(toMails);

            SmtpClient client = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings.Get("smtpHost"),
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings.Get("smtpLogin"), ConfigurationManager.AppSettings.Get("smtpPassword")),
                Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("smtpPort")),
                EnableSsl = true
            };

            int retry = 3;
            do
            {
                try
                {
                    client.Send(mail);
                    return true;
                }
                catch (Exception ex) { }

                retry--;
            } while (retry > 0);

            return false;
        }
    }
}