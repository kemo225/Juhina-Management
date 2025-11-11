using Juhyna_BLL.CustomerPlace.InterFace;
using Juhyna_DAL.CustomerPlace.DTO;
using Juhyna_DAL.CustomerPlace.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.CustomerPlace.CustomerPlace
{
    public class ClsCustomerPlace:ICustomerPlaceBLL
    {
        private readonly ICustomerPlace _CustomerPlace;
        public ClsCustomerPlace(ICustomerPlace customerPlace)
        {
            _CustomerPlace = customerPlace;
        }
        public List<DTOCustomerPlaceRead> GetShopsByCistomerID(int CustomerID)
        {
            return _CustomerPlace.GetShopsByCistomerID(CustomerID);
        }
        public List<DTOCustomerPlaceRead> GetShopsCustomers()
        {
            return _CustomerPlace.GetShopsCustomers();
        }
        public DTOCustomerPlaceRead GetShopsCustomerByID(int ID)
        {
            return _CustomerPlace.GetShopsCustomerByID(ID);
        }
       public bool DeleteShopsCustomerby(int ID,string Email)
        {
            return _CustomerPlace.DeleteShopsCustomerby(ID, Email);
        }
        public DTOCustomerPlacereAdd AddPlaceToCustomer(DTOCustomerPlaceAdd customer)
        {
            return _CustomerPlace.AddPlaceToCustomer(customer);
        }
    }
}
