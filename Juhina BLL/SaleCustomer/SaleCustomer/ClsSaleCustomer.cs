using Juhyna_BLL.SaleCustomer.InterFace;
using Juhyna_DAL.SaleCustomer.DTO;
using Juhyna_DAL.SaleCustomer.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.SaleCustomer.SaleCustomer
{
    public class ClsSaleCustomer : ISaleCustomerBLL
    {
        private readonly ISaleCustomer _SaleCustomer;
        public ClsSaleCustomer(ISaleCustomer saleCustomer)
        {
            _SaleCustomer = saleCustomer;
        }
        public List<DtoSaleCustomerRead> GetSaleRelatedCustomer()
        {
            return _SaleCustomer.GetSaleRelatedCustomer();
        }
        public List<DtoSaleCustomerRead> GetSalesRelatedByCustomerID(int CustomerID)
        {
            return _SaleCustomer.GetSalesRelatedByCustomerID(CustomerID);
        }
        public List<DtoSaleCustomerRead> GetCustomerRelatedBySaleID(int SaleID)
        {
            return _SaleCustomer.GetCustomerRelatedBySaleID(SaleID);
        }
        public List<DtoSaleCustomerRead> GetSalesRelatedByCustomerName(string FirstName)
        {
            return _SaleCustomer.GetSalesRelatedByCustomerName(FirstName);
        }
        public List<DtoSaleCustomerRead> GetCustomerSRelatedBySaleName(string FirstName)
        {
            return _SaleCustomer.GetCustomerSRelatedBySaleName(FirstName);
        }
        public DtoSaleCustomerRead GetSaleCustomerByID(int ID)
        {
            return _SaleCustomer.GetSaleCustomerByID(ID);
        }
        public DTOSaleCustomerUpdate UpdateSaleCustomer(DTOSaleCustomerUpdate saleCustomerUpdate)
        {
            var saleCustomer = _SaleCustomer.UpdateSaleCustomer(saleCustomerUpdate);
            if (saleCustomer != null)
                return saleCustomer;
            else
                return null;
        }
        public DTOSaleCustomerUpdate AddSaleCustomer(DTOSaleCustomerAdd saleCustomerAdd)
        {
            var saleCustomer = _SaleCustomer.AddSaleCustomer(saleCustomerAdd);
            if (saleCustomer != null)
                return saleCustomer;
            else
                return null;
        }
        public bool DeleteSaleCustomer(int ID)
        {
            if (_SaleCustomer.DeleteSaleCustomer(ID))
                return true;
            else
                return false;
        }


    }
}
