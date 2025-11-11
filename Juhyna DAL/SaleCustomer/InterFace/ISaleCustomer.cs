using Juhyna_DAL.Models;
using Juhyna_DAL.SaleCustomer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.SaleCustomer.InterFace
{
    public interface ISaleCustomer
    {
        public List<DtoSaleCustomerRead> GetSaleRelatedCustomer();
        public List<DtoSaleCustomerRead> GetCustomerRelatedBySaleID(int ID);
        public List<DtoSaleCustomerRead> GetCustomerSRelatedBySaleName(string FirstName);
        public List<DtoSaleCustomerRead> GetSalesRelatedByCustomerName(string FirstName);
        public List<DtoSaleCustomerRead> GetSalesRelatedByCustomerID(int ID);
        public bool DeleteSaleCustomer(int ID);
        public DTOSaleCustomerUpdate AddSaleCustomer(DTOSaleCustomerAdd SaleCustomer);

        public DTOSaleCustomerUpdate UpdateSaleCustomer(DTOSaleCustomerUpdate SaleCustomerUpdate);
        public DtoSaleCustomerRead GetSaleCustomerByID(int ID);

    }
}
