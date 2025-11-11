using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProductInInventoryByAdminstrative.DTO
{
    public class DtoManageProductWithAdminstrativeRead
    {
        public int ID {  get; set; }
        public string CraetedByAdmimstrative { get; set; }
        public DateOnly CreateAt { get; set; }
        public string ProductName {  get; set; }
        public int FillQuantity {  get; set; }
        public string InventoryName { get; set; }
        public string InventoryLocation { get; set; }

    }
}
