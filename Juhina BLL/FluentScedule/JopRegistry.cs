using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.FluentScedule
{
    public class JopRegistry:Registry
    {
        public JopRegistry()
        {
            Schedule<DailyProfitNet>().ToRunEvery(1).Days().At(0, 0);
        }
    }
}
