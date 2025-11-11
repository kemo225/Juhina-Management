using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Return.DTO
{
    public class DtoReturnRead
    {
        public int ID { get; set; }
        public string AdminstrativeName { get; set; }
        public string CraetedBySaleName { get; set; }
        public bool IsConfirmed { get; set; }
        public DateOnly ConfirmedAt { get; set; }
        public DateOnly CreatedAt { get; set; }
        public int QuantityTaked { get; set; }
        public string ProductName { get; set; }
        public int Back { get; set; }
    }
}
