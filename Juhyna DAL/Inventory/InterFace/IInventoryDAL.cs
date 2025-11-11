using Juhyna_DAL.Inventory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Inventory.InterFace
{
    public interface IInventoryDAL
    {
        public List<DtoInventoryRead> GetAllInventories();
        public DtoInventoryRead GetInventoryByID(int ID);
        public int AddInventory(DtoInventoryAdd inventory);
        public bool DeleteInventory(int ID);

    }
}
