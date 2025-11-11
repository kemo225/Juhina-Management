using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProducts.DTO
{
    public class DtoProductInventoryUpdate
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Bought { get; set; }
        public int WaitingQuantity { get; set; }
        public int InventoryQuantity { get; set; }

    }
}
