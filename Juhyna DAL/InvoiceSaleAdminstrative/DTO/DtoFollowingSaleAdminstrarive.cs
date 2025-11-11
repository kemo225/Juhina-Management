using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.InvoiceSaleAdminstrative.DTO
{
    public class DtoFollowingSaleAdminstrarive
    {
        public int Id { get; set; }
        public int productinInventoryID { get; set; }
        public DateOnly CreateAt { get; set; }
           public int Quantity { get; set; }
       public int Rest { get; set; }
        public int Bought { get; set; }
        public int CreatedByAdminID { get; set; }
        public int SaleID { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
