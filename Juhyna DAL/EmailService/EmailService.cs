using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.EmailService
{
    public class EmailService
    {
        private static readonly string EmailFrom = "Ka4766311@gmail.com";

        private static readonly string Password = "vlra bsav tecz guur";
        public static void SendEmail(string Emailto, string subject, string body)
        {
            try
            {
                // Implement email sending logic here using EmailFrom and Password
                // This is a placeholder for the actual email sending code
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,// SMTP port for mails
                    Credentials = new NetworkCredential(EmailFrom, Password),
                    EnableSsl = true,// for connection security
                };
                var MailMessage = new MailMessage
                {
                    From = new MailAddress(EmailFrom),
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
