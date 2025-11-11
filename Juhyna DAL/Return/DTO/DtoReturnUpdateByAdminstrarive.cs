using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Return.DTO
{
    public class DtoReturnUpdateByAdminstrarive
    {
        public int ID { get; set; }
        public DateOnly ConfirmedAt { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
