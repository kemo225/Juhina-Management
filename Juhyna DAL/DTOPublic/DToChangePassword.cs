using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.DTOPublic
{
    public class DToChangePassword
    {
        public int ID { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
