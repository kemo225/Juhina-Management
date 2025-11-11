using Juhyna_DAL.Admins.InterFace;
using Juhyna_DAL.EmailService.InterFce;
using Juhyna_DAL.Reports.InterFace;
using Juhyna_DAL.Reports.PrepareReprots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Reports.ReportSend
{
    public class ReportSend: IReportSend
    {
        private readonly IAdminDAL _admins;
        private readonly INotfifcationDAL _notfifcationDAL;
        public ReportSend(INotfifcationDAL notfifcationDAL,IAdminDAL admins)
        {
            _admins = admins;
            _notfifcationDAL= notfifcationDAL;
        }
        public void SendReportDailtyforAllAdmins()
        {
            var admins = _admins.GetAdmins();
           if (admins == null || admins.Count == 0)
                return;  
           
            foreach (var admin in admins)
            {
                _notfifcationDAL.SendEmail(admin.Email, "Daily Report", $"Dear [{admin.FirstName + " " + admin.LastName}],\r\n\r\nI hope this email finds you well.\r\n\r\nPlease find below the net profit details for [{DateTime.Now}]:\r\n\r\nNet Profit: [{PrepareReport.GetNetProfitToday().ToString()}]\r\n\r\n[Optional: Any brief notes or highlights, e.g., \"The increase in sales in [Product/Service] contributed to a higher net profit today.\"]\r\n\r\nPlease let me know if you need any further details or breakdowns.\r\n\r\nThank you.\r\n\r\nBest regards,\r\n[Juhina Company]");
            }
        }
    }
}
