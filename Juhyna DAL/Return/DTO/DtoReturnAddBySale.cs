using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.Return.DTO
{
    public class DtoReturnAddBySale
    {
        public int AdminstrativeId { get; set; }
        [JsonIgnore]
        public int CraetedBySaleId { get; set; }
        public int Back { get; set; }
        public int InvoiceID { get; set; }
        [JsonIgnore]
        public DateOnly CreatedAt { get; set; }

    }
}
