using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.ProductInventory.DTO
{
    public class DtoProductInventoryAddReturned
    {
        public int ID {  get; set; }
        public int ProductID { get; set; }
        public int InventoryID { get; set; }
        public int QuantityTotal { get; set; }

        public int WaitingQuantity { get; set; }


        public int InventoryQuantity { get; set; }

        public int Rest { get; set; }

        public int bought { get; set; }

    }
}
