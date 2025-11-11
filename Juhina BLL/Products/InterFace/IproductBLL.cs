using Juhyna_DAL.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Products.InterFace
{
    public interface IproductBLL
    {
        public List<DTOProducctRead> GetAllProducts();
        public DTOProducctRead GetProductsByID(int ID);
        public DTOProducctRead UpdateProduct(dtoproductUpdate obj);
        public DTOProducctRead AddProduct(DTOproductAdd obj);
    }
}
