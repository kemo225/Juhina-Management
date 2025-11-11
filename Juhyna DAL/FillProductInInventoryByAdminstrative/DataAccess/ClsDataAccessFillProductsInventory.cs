using AutoMapper;
using Juhyna_DAL.ManageProductInInventoryByAdminstrative.DTO;
using Juhyna_DAL.ManageProductInInventoryByAdminstrative.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.Visits.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProductInInventoryByAdminstrative.DataAccess
{
    public class ClsDataAccessFillProductsInventory:IManagementProductInentoryDAl
    {
        private readonly JuhinaDBContext _JuhinaDB;
        private readonly IMapper _Mapper;
        public ClsDataAccessFillProductsInventory(JuhinaDBContext JuhinaDB,IMapper mapper)
        {
            _JuhinaDB = JuhinaDB;
            _Mapper = mapper;
        }
        public bool FillProductinInventory(DtoFillProductInventory dto)
        {
            try
            {
                var productinevntory = _JuhinaDB.ProductInventories.Find(dto.ProductInventoryID);
                if (productinevntory == null)
                    return false;
                var ManageProductWithAdminstrative = new Models.FillProductinInventory()
                {
                    ProductInventoryID = dto.ProductInventoryID,
                    CreateAt = DateOnly.FromDateTime(DateTime.Now),
                    CreatedbyAdminstrativeID = dto.CreatedByAdminstrativeID,
                    FillQuantity= dto.FillQuantity,
                    ProductInventory = productinevntory,
                };
                _JuhinaDB.Add(ManageProductWithAdminstrative);
                _JuhinaDB.SaveChanges();
                ManageProductWithAdminstrative.ProductInventory.QuantityTotal += dto.FillQuantity;
                ManageProductWithAdminstrative.ProductInventory.InventoryQuantity += dto.FillQuantity;

                _JuhinaDB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //public bool ExchangeProductinInventory(DtoExcahngeProductInventory dto)
        //{
        //    try
        //    {
        //        var productinevntory = _JuhinaDB.ProductInventories.Find(dto.ProductInventoryID);
        //        if (productinevntory == null)
        //            return false;
        //        var ManageProductWithAdminstrative = new Models.ManageAdminstrativeForProductinInventory()
        //        {
        //            ProductInventoryID = dto.ProductInventoryID,
        //            CreateAt = DateOnly.FromDateTime(DateTime.Now),
        //            CreatedbyAdminstrativeID = dto.CreatedByAdminstrativeID,
        //            ProductInventory = productinevntory
        //            , Status = Convert.ToInt32(enumStatus.Exchange)
        //        };
        //        _JuhinaDB.Add(ManageProductWithAdminstrative);
        //        _JuhinaDB.SaveChanges();
        //        if (ManageProductWithAdminstrative.ProductInventory.Quantity < dto.ExchangeQuantity)
        //            return false;
        //        ManageProductWithAdminstrative.ProductInventory.Quantity -= dto.ExchangeQuantity;
        //        ManageProductWithAdminstrative.ProductInventory.Bought += dto.ExchangeQuantity;
        //        ManageProductWithAdminstrative.ProductInventory.Rest -= dto.ExchangeQuantity;

        //        _JuhinaDB.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public List<DtoManageProductWithAdminstrativeRead> GetAllManageProductsByAdminstrative()
        {
            try { 
            var manageProducts =_JuhinaDB.FillProductinInventories.Include(m => m.Adminstrative).ThenInclude(a => a.Person)
                .Include(m => m.ProductInventory).ThenInclude(p => p.Product).Include(f=>f.ProductInventory).ThenInclude(i=>i.Inventory)
              .AsNoTracking().ToList();
            if (manageProducts.Count <= 0)
                return null;
                return _Mapper.Map<List<DtoManageProductWithAdminstrativeRead>>(manageProducts);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
