using Juhyna_BLL.Customers.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Admins.InterFace;
using Juhyna_DAL.Customer.DTO;
using Juhyna_DAL.Customer.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Customers.Customers
{
    public class ClsCustomer:ICustomerBLL
    {
        private readonly ICustomer _Customer;
        public ClsCustomer(ICustomer customer)
        {
            _Customer = customer;
        }
        public List<DTOCustmerRead> GetCustomers()
        {
            return _Customer.GetCustomers();
        }
        public DTOCustmerRead GetCustomerBYID(int id)
        {
            var Customer = _Customer.GetCustomerReadBYID(id);
            if (Customer != null)
                return Customer;
            return null;
        }
        public DtoCustmerUpdate AddCustomer(DTOCustmerAdd ad)
        {
            var Customer = _Customer.AddCustomer(ad);
            if (Customer != null)
                return Customer;
            return null;
        }
        public DtoCustmerUpdate UpdateCustomer(DtoCustmerUpdate ad)
        {
            var Customer = _Customer.UpdateCustomer(ad);
            if (Customer != null)
                return Customer;
            return null;
        }
        public bool DeleteCustomer(int id)
        {
            return _Customer.DeleteCustomer(id);
        }
        public bool IsCustomerExist(int ID)
        {
            return _Customer.IsCustomerExist(ID);
        }
    }
}
