using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.InvoiceSaleAdminstrative.DTO
{
    public class DtoFollowingSaleAdminstrativeAddforadminstrativeReturned
    {
        public int ID { get; set; }
        public int ProductinInventoryID { get; set; }
        [JsonIgnore]
        public DateTime CreateAt { get; set; }
        public int Quantity { get; set; }
        public int CreatedByAdminstartiveID { get; set; }
        public int SaleID { get; set; }
        public string ProductName { get; set; } //DataBase Name Description
        public int productprice { get; set; }
        public string InventoryName { get; set; }
        public string InventoryAddress { get; set; }
        public string CreatedByAdminstrativeName { get; set; }
    }
}
