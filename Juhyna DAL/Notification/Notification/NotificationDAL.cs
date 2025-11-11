using Juhyna_DAL.EmailService.InterFce;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.EmailService.NewFolder
{
    public class NotificationDAL:INotfifcationDAL
    {

        private readonly IConfiguration _Configure;
        public NotificationDAL(IConfiguration configure)
        {
            _Configure = configure;
        }
        public void SendEmail(string Emailto, string subject, string body)
        {
            try
            {
                // Implement email sending logic here using EmailFrom and Password
                // This is a placeholder for the actual email sending code
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,// SMTP port for mails
                    Credentials = new NetworkCredential(_Configure["EmailFrom"], _Configure["PasswordEmail"]!),
                    EnableSsl = true,// for connection security
                };
                var MailMessage = new MailMessage
                {
                    From = new MailAddress(_Configure["EmailFrom"]!),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                MailMessage.To.Add(Emailto);

                smtpClient.Send(MailMessage);
            }
            catch (Exception)
            {
                return;
            }





        }
    }
}
