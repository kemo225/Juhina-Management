using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Inventory.Dto
{
    public class DtoInventoryReadDetails
    {
        public int ID { get; set; }
        public string InventoryName { get; set; } 
        public string InventoryAddress { get; set; } 
        public List<DtoAdminstrativeInventory> AdminstrativesWork { get; set; } = new List<DtoAdminstrativeInventory>();

    }

}
