using Juhyna_BLL.EmailService.InterFace;
using Juhyna_DAL.EmailService.InterFce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.NotificationService.Notification
{
    public class NotificationBLL:InotificationBLL
    {
   private readonly INotfifcationDAL _Notification;
        public NotificationBLL(INotfifcationDAL Notification)
        {
            _Notification = Notification;
        }
        public  void SendEmail(string Emailto, string subject, string body)
        {
           _Notification.SendEmail(Emailto, subject, body);    
        }
    }
}
