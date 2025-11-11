using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Adminstrative.DTO;
using Juhyna_DAL.Customer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Customer.InterFace
{
    public interface ICustomer
    {
        public List<DTOCustmerRead> GetCustomers();
        public DTOCustmerRead GetCustomerReadBYID(int id);
        public DtoCustmerUpdate AddCustomer(DTOCustmerAdd admin);
        public DtoCustmerUpdate UpdateCustomer(DtoCustmerUpdate admin);
        public bool DeleteCustomer(int ID);
        public bool IsCustomerExist(int id);
    }
}
