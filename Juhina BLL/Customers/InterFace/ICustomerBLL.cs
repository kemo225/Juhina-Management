using Juhyna_DAL.Customer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Customers.InterFace
{
    public interface ICustomerBLL
    {
        public List<DTOCustmerRead> GetCustomers();
        public DTOCustmerRead GetCustomerBYID(int id);
        public DtoCustmerUpdate AddCustomer(DTOCustmerAdd Customer);
        public DtoCustmerUpdate UpdateCustomer(DtoCustmerUpdate Customer);
        public bool DeleteCustomer(int ID);
        public bool IsCustomerExist(int id);
    }
}
