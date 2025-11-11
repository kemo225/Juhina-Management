using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProducts.DTO
{
    public class DtoProductInventoryRead
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string InventoryName { get; set; }
        public int Quantity { get; set; }
        public int Bought { get; set; }
        public int Rest { get; set; }
        public int WaitingQuantity { get; set; }
        public int InventoryQuantity { get; set; }



    }
}
