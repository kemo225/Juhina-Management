using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProducts.DTO
{
    public class DtoProductInventoryAdd
    {
        public int ProductID { get; set; }
        public int InventoryID { get; set; }
        [JsonIgnore]
        public int Quantity { get; set; }
        [JsonIgnore]

        public int WaitingQuantity { get; set; }
        [JsonIgnore]


        public int InventoryQuantity { get; set; }
        [JsonIgnore]

        public int Rest {  get; set; }
        [JsonIgnore]

        public int bought {  get; set; }

    }
}
