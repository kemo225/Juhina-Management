using FluentScheduler;
using Juhyna_BLL.EmailService;
using Juhyna_DAL.Reports.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.FluentScedule
{
    public class DailyProfitNet:IJob
    {
        private readonly IReportSend reportSend;
        public DailyProfitNet(IReportSend reportSend)
        {
            this.reportSend=reportSend;
        }
        public void Execute()
        {
            try
            { 
        reportSend.SendReportDailtyforAllAdmins();
            }
            catch(Exception ex)
            {
                // Log the exception (you can use any logging framework you prefer)
              
            }
        }
    }
}
