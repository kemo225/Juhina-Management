using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.Orders.DTO
{
    public class DtoOrderAdd
    {
        public int InvoiceID { get; set; }
        public int VisitID { get; set; }
        [JsonIgnore]
        public int TotalPrice { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public int ProductPrice { get; set; }
        public int PaymentmethodID { get; set; }
        [JsonIgnore]
        public DateOnly CreateAt { get; set; }
        [JsonIgnore]
        public int CreatedBySaleID { get; set; }
        [JsonIgnore]
        public int Status { get; set; }
    }
}
