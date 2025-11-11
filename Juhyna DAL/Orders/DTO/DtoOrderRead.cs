using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Orders.DTO
{
    public class DtoOrderRead
    {
        public int Id { get; set; }
        public int InvoiceID { get; set; }
        public string CustomerName { get; set; }
        public string SaleName { get; set; }
        public string AddressPlace { get; set; }
        public string ProductName { get; set; } 
        public int Quantity { get; set; }
        public int ProductPrice {  get; set; }
        public int TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string PhoneCustomer { get; set; }
        public string EmailCustomer { get; set; }
        public string Status { get; set; }  
        public DateOnly CreateAt { get; set; }

    }
}
