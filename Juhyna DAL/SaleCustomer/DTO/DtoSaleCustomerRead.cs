using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.SaleCustomer.DTO
{
    public class DtoSaleCustomerRead
    {
        public int ID { get; set; }
        public string SaleName { get; set; }
        public string CustomerName { get; set; }
public List<DtoCustomerPlceRead> CustomerPlces { get; set; }
    }
}
