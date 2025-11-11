using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.FollowingSaleAdminstrative.DTO
{
    public class DtoFollowingSaleAdminstrativeAddforadminstrative
    {
        public int ProductinInventoryID { get; set; }
        [JsonIgnore]
        public DateTime CreateAt { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public int CreatedByAdminstrativeID { get; set; }
        public int SaleID { get; set; }
    }
}
