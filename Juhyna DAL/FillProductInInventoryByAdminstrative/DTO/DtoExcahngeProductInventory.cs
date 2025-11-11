using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProductInInventoryByAdminstrative.DTO
{
    public enum enumStatus
    {
        Fill=1,
        Exchange=2
    }   
    public class DtoExcahngeProductInventory
    {
        public int ExchangeQuantity { get; set; }
        public int CreatedByAdminstrativeID { get; set; }

        public int ProductInventoryID { get; set; }
        [JsonIgnore]
        public int Status { get; set; }
        [JsonIgnore]

        public DateOnly CreateAt { get; set; }

    }
}
