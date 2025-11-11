using Juhyna_DAL.ManageProducts.DTO;
using Juhyna_DAL.Models;
using Juhyna_DAL.ProductInventory.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProducts.InterFace
{
    public interface IProductInventoryDAL
    {
        public List<DtoProductInventoryRead> GetAllProductinInventories();
        public DtoProductInventoryRead GetAllProductsInInventoryByID(int ID);
        public List<DtoProductInventoryRead> GetAllProductsInInventory(int InventoryID);
        public List<DtoProductInventoryRead> GetProductInfoInInventory(int ProductID, int InventoryID);
        public DtoProductInventoryAddReturned AddProductToInventory(DtoProductInventoryAdd obj);
        public bool ExchangefromProductininventory(int productininventoryID, int QuantityExchange);

        public bool FillfromProductininventory(int productininventoryID, int QuantityCharge);
    }
}