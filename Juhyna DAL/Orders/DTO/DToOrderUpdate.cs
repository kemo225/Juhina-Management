using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.Orders.DTO
{
    public class DToOrderUpdate
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public int VisitID { get; set; }
        public int Quantity { get; set; }
        public int PaymentmethodID { get; set; }
        public int CreatedBySaleID { get; set; }
    }
}
