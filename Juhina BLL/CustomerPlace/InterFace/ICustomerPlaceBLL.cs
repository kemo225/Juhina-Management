using Juhyna_DAL.CustomerPlace.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.CustomerPlace.InterFace
{
    public interface ICustomerPlaceBLL
    {
        public List<DTOCustomerPlaceRead> GetShopsByCistomerID(int CustomerID);
        public List<DTOCustomerPlaceRead> GetShopsCustomers();
        public DTOCustomerPlaceRead GetShopsCustomerByID(int ID);
        public bool DeleteShopsCustomerby(int ID, string Email);
        public DTOCustomerPlacereAdd AddPlaceToCustomer(DTOCustomerPlaceAdd dTOSaleCustomerAdd);
    }
}
