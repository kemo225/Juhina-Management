using Juhyna_DAL.CustomerPlace.DTO;
using Juhyna_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.CustomerPlace.InterFace
{
    public interface ICustomerPlace
    {
        public List<DTOCustomerPlaceRead> GetShopsByCistomerID(int CustomerID);

        public List<DTOCustomerPlaceRead> GetShopsCustomers();
        public DTOCustomerPlaceRead GetShopsCustomerByID(int ID);
        public bool DeleteShopsCustomerby(int ID, string Email);
        public DTOCustomerPlacereAdd AddPlaceToCustomer(DTOCustomerPlaceAdd dTOSaleCustomerAdd);
    }
}
