using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.EmailService.InterFace
{
    public interface InotificationBLL
    {
        public void SendEmail(string Emailto, string subject, string body);

    }
}
