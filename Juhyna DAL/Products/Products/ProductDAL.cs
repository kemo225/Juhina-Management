using AutoMapper;
using Juhyna_DAL.ManageProducts.DTO;
using Juhyna_DAL.ManageProducts.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.ProductInventory.DTO;
using Juhyna_DAL.Products.Dto;
using Juhyna_DAL.Products.InterFace;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Products.Products
{
    public class ProductDAL : IproductDAL
    {
        private readonly JuhinaDBContext _JuhinaDB;
        private readonly IMapper _Mapper;
        public ProductDAL(JuhinaDBContext JuhinaDB, IMapper mapper)
        {
            _Mapper = mapper;
            _JuhinaDB = JuhinaDB;
        }

        public List<DTOProducctRead> GetAllProducts()
        {
            var Products = _JuhinaDB.Products.AsNoTracking().ToList();
            if (Products.Count <= 0)
                return null;
            else
                return _Mapper.Map<List<DTOProducctRead>>(Products);
        }
        public DTOProducctRead GetProductsByID(int ID)

        {
            var SaleCustmers = _JuhinaDB.ProductInventories.Where(P => P.ID == ID).FirstOrDefault();
            if (SaleCustmers == null)
                return null;
            else
                return _Mapper.Map<DTOProducctRead>(SaleCustmers);
        }
        public DTOProducctRead UpdateProduct(dtoproductUpdate obj)
        {
            try
            {
                var objUpdate = _JuhinaDB.Products.Where(P => P.ID == obj.ID).FirstOrDefault();
                if (objUpdate != null)
                {
                    objUpdate.Price = obj.Price;
                    _JuhinaDB.SaveChanges();
                    return _Mapper.Map<DTOProducctRead>(objUpdate);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DTOProducctRead AddProduct(DTOproductAdd obj)
        {
            try
            {
                var objAdd = new Models.Product()
                {
                    Size = obj.size,
                    Description = obj.Description,
                    Price = obj.Price,
                    CategoryID = obj.CategoryID
                };
                _JuhinaDB.Products.Add(objAdd);
                _JuhinaDB.SaveChanges();
                return _Mapper.Map<DTOProducctRead>(objAdd);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       

        

    }
}
