using Juhyna_DAL.Models;
using Juhyna_DAL.Reports.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Reports.PrepareReprots
{
    public static class PrepareReport
    {
        private static readonly JuhinaDBContext _context;
     
        public static int GetNetProfitToday()
        {
            var NetprofitToday = _context.Orders
                .Where(o => o.CreateAt.Date == DateTime.Now.Date).Max(O=>O.TotalPrice);

            // Placeholder implementation
            return NetprofitToday;
        }
    }
}
