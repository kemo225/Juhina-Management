using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.Return.DTO
{
    public class DtoReturnUpdateBySale
    {
        public int ID { get; set; }
        public int AdminstrativeId { get; set; }
        [JsonIgnore]
        public int CraetedBySaleId { get; set; }
        public int ProductId { get; set; }
        public int Back { get; set; }
        public int InvoiceId { get; set; }
        [JsonIgnore]
        public DateOnly CreatedAt { get; set; }

    }
}
