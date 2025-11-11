using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.EmailService
{
    public static class EmailServiceBLL
    {
   
        public static void SendEmail(string Emailto, string subject, string body)
        {
           EmailService.SendEmail(Emailto, subject, body);    
        }
    }
}
