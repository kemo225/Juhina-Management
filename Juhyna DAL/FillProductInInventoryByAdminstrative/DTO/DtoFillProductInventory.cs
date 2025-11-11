using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProductInInventoryByAdminstrative.DTO
{
    public class DtoFillProductInventory
    {
        public int FillQuantity {  get; set; }
        [JsonIgnore]
        public int CreatedByAdminstrativeID { get; set; }
        public int ProductInventoryID { get; set; }

        [JsonIgnore]
        public DateOnly CreateAt { get; set; }


    }
}
