using AutoMapper;
using Juhyna_DAL.ManageProductInInventoryByAdminstrative.DTO;
using Juhyna_DAL.ManageProducts.DTO;
using Juhyna_DAL.ManageProducts.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.ProductInventory.DTO;
using Juhyna_DAL.SaleCustomer.DTO;
using Juhyna_DAL.SaleCustomer.InterFace;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.ProductInventory.DataAcceaaManageProduct
{
    public class ClsDataAccessProductInevntory: IProductInventoryDAL
    {
        private readonly JuhinaDBContext _JuhinaDB;
        private readonly IMapper _Mapper;
        public ClsDataAccessProductInevntory(JuhinaDBContext JuhinaDB, IMapper mapper)
        {
            _Mapper = mapper;
            _JuhinaDB = JuhinaDB;
        }

        public List<DtoProductInventoryRead> GetAllProductinInventories()
        {
            var SaleCustmers = _JuhinaDB.ProductInventories.AsNoTracking().Include(P => P.Inventory).Include(P => P.Product).ToList();
            if (SaleCustmers.Count <= 0)
                return null;
            else
            return _Mapper.Map<List<DtoProductInventoryRead>>(SaleCustmers);    
        }

         public DtoProductInventoryRead GetAllProductsInInventoryByID(int ID)

        {
            var SaleCustmers = _JuhinaDB.ProductInventories.Include(P => P.Inventory).Include(P => P.Product).Where(P=>P.ID==ID).FirstOrDefault();
            if (SaleCustmers==null)
                return null;
            else
                return _Mapper.Map<DtoProductInventoryRead>(SaleCustmers);
        }

        public List<DtoProductInventoryRead> GetAllProductsInInventory(int InventoryID)
        {
            var SaleCustmers = _JuhinaDB.ProductInventories.Include(P => P.Inventory).Include(P => P.Product).Where(P=>P.InventoryID==InventoryID).ToList();
            if (SaleCustmers.Count <= 0)
                return null;
            else
                return _Mapper.Map<List<DtoProductInventoryRead>>(SaleCustmers);
        }

        public List<DtoProductInventoryRead> GetProductInfoInInventory(int ProductID,int InventoryID)
        {
            var SaleCustmers = _JuhinaDB.ProductInventories.Include(P => P.Inventory).Include(p=>p.Product).Where(p=>p.ProductID==ProductID&&p.InventoryID==InventoryID).ToList();
            if (SaleCustmers.Count <= 0)
                return null;
            else
                return _Mapper.Map<List<DtoProductInventoryRead>>(SaleCustmers);
        }

       public DtoProductInventoryAddReturned AddProductToInventory(DtoProductInventoryAdd obj)
        {
            try
            {
                var objAdd = new Models.ProductInventory()
                {
                    ProductID = obj.ProductID,
                    InventoryID = obj.InventoryID,
                    QuantityTotal = 0,
                    Rest = 0,
                    Bought = 0,
                    WaitingQuantity = 0,
                    InventoryQuantity = 0

                };
                _JuhinaDB.ProductInventories.Add(objAdd);
                _JuhinaDB.SaveChanges();
                return _Mapper.Map<DtoProductInventoryAddReturned>(objAdd);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool ExchangefromProductininventory(int productininventoryID, int QuantityExchange)
        {
            var ProductinInventory = _JuhinaDB.ProductInventories.Find(productininventoryID);
            if (ProductinInventory == null)
                return false;

            ProductinInventory.Bought += QuantityExchange;
            ProductinInventory.WaitingQuantity -= QuantityExchange;

            _JuhinaDB.SaveChanges();
            return true;
        }

        public bool FillfromProductininventory(int productininventoryID, int QuantityCharge)
        {
            var ProductinInventory = _JuhinaDB.ProductInventories.Find(productininventoryID);
            if (ProductinInventory == null)
                return false;

            ProductinInventory.Bought -= QuantityCharge;
            ProductinInventory.WaitingQuantity += QuantityCharge;

            _JuhinaDB.SaveChanges();
            return true;
        }

    }
}
