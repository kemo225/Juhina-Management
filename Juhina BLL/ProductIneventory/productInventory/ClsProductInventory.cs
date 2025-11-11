using Juhyna_BLL.ProductIneventory.InterFace;
using Juhyna_DAL.ManageProducts.DTO;
using Juhyna_DAL.ManageProducts.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.ProductInventory.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.ProductIneventory.productInventory
{
    public class ClsProductInventory: IproductInevtoryBLL
    {
        private readonly IProductInventoryDAL _IProductInventoryDAL;
        public ClsProductInventory(IProductInventoryDAL productInventoryDAL)
        {
            _IProductInventoryDAL = productInventoryDAL;
        }
        public List<DtoProductInventoryRead> GetAllProductinInventories()
        {
                       return _IProductInventoryDAL.GetAllProductinInventories();
        }
        public DtoProductInventoryRead GetAllProductsInInventoryByID(int ID)

        {
            return _IProductInventoryDAL.GetAllProductsInInventoryByID(ID);
        }

        public List<DtoProductInventoryRead> GetAllProductsInInventory(int InventoryID)
        {
          return  _IProductInventoryDAL.GetAllProductsInInventory(InventoryID);
        }

        public List<DtoProductInventoryRead> GetProductInfoInInventory(int ProductID, int InventoryID)
        {
           
            return _IProductInventoryDAL.GetProductInfoInInventory(ProductID, InventoryID);
        }

        public DtoProductInventoryAddReturned AddProductToInventory(DtoProductInventoryAdd obj)
        {
         return   _IProductInventoryDAL.AddProductToInventory(obj);
        }

    }
}
