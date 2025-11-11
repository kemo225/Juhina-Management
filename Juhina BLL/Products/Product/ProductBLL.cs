using Juhyna_BLL.Products.InterFace;
using Juhyna_DAL.Products.Dto;
using Juhyna_DAL.Products.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Products.Product
{
    public class ProductBLL: IproductBLL
    {
        private readonly IproductDAL _ProductDAL;
        public ProductBLL(IproductDAL productDAL)
        {
            _ProductDAL = productDAL;
        }
        public List<DTOProducctRead> GetAllProducts()
        {
            return _ProductDAL.GetAllProducts();
        }
        public DTOProducctRead GetProductsByID(int ID)
        {
            return _ProductDAL.GetProductsByID(ID);
        }
        public DTOProducctRead UpdateProduct(dtoproductUpdate obj)
        {
            return _ProductDAL.UpdateProduct(obj);
        }
        public DTOProducctRead AddProduct(DTOproductAdd obj)
        {
            return _ProductDAL.AddProduct(obj);
        }

    }
}
