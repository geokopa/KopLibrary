using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace KopLibrary.Helpers
{
    public class EmailHelper
    {
        public string Smtp { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string From { get; set; }
        public string Domain { get; set; }
        public string DisplayName { get; set; }

        public bool SendMail(string recepient, string subject, string message)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(Smtp) && !string.IsNullOrEmpty(SmtpUser) &&
                !string.IsNullOrEmpty(SmtpPassword))
            {
                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential = new NetworkCredential(SmtpUser, SmtpPassword,
                    Domain);

                smtpClient.Host = Smtp;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;

                var mail = new MailMessage(From, recepient.Trim())
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                    From = new MailAddress(From, DisplayName)
                };

                try
                {
                    smtpClient.Send(mail);
                    result = true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return result;
        }
    }
}
