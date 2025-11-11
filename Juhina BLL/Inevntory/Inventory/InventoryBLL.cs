using AutoMapper;
using Juhyna_BLL.Inevntory.InterFace;
using Juhyna_DAL.Inventory.Dto;
using Juhyna_DAL.Inventory.InterFace;
using Juhyna_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Inevntory.Inventory
{
    public class InventoryBLL: IInventoryBLL
    {
        private readonly IInventoryDAL _inventoryDal;
        public InventoryBLL(IInventoryDAL inventoryBLL)
        {
_inventoryDal = inventoryBLL;
        }
    
        public List<DtoInventoryRead> GetAllInventories()
        {
            return _inventoryDal.GetAllInventories();
        }

        public DtoInventoryRead GetInventoryByID(int ID)
        {
            return _inventoryDal.GetInventoryByID(ID);
        }
        public int AddInventory(DtoInventoryAdd inventory)
        {
          return  _inventoryDal.AddInventory(inventory);
        }
        public bool DeleteInventory(int ID)
        {
            return _inventoryDal.DeleteInventory(ID);
        }
    }
}
