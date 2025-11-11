using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.EmailService.InterFce
{
    public interface INotfifcationDAL
    {
        public void SendEmail(string Emailto, string subject, string body);

    }
}
