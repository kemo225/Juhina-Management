using Juhyna_DAL.SaleCustomer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.SaleCustomer.InterFace
{
    public interface ISaleCustomerBLL
    {
        public DtoSaleCustomerRead GetSaleCustomerByID(int ID);
        public List<DtoSaleCustomerRead> GetSaleRelatedCustomer();
        public List<DtoSaleCustomerRead> GetCustomerRelatedBySaleID(int ID);
        public List<DtoSaleCustomerRead> GetCustomerSRelatedBySaleName(string FirstName);
        public List<DtoSaleCustomerRead> GetSalesRelatedByCustomerName(string FirstName);
        public List<DtoSaleCustomerRead> GetSalesRelatedByCustomerID(int ID);
        public bool DeleteSaleCustomer(int ID);
        public DTOSaleCustomerUpdate AddSaleCustomer(DTOSaleCustomerAdd SaleCustomer);

        public DTOSaleCustomerUpdate UpdateSaleCustomer(DTOSaleCustomerUpdate SaleCustomerUpdate);

    }
}
