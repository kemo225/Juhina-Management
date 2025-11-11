using AutoMapper;
using Juhyna_DAL.CustomerPlace.DTO;
using Juhyna_DAL.CustomerPlace.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.SaleCustomer.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.CustomerPlace.DataAccessCustomerPlaces
{
    public class ClsDataAccessCustomerPlace:ICustomerPlace
    {
        private readonly JuhinaDBContext _juhinaDB;
        private readonly IMapper _mapper;
        public ClsDataAccessCustomerPlace(JuhinaDBContext juhinaDB, IMapper mapper)
        {
            _juhinaDB = juhinaDB;
            _mapper = mapper;
        }
        public List<DTOCustomerPlaceRead> GetShopsCustomers()
        {
            var CustomerPlaceReturn =new List<DTOCustomerPlaceRead>();
            var ShopsFrCustomer = _juhinaDB.CustomerPlaces.Include(CP => CP.Customer).ThenInclude(C=>C.Person).Include(CP => CP.Place).AsNoTracking().ToList();
            foreach (var shop in ShopsFrCustomer)
            {
                CustomerPlaceReturn.Add(_mapper.Map<DTOCustomerPlaceRead>(shop));
            }
            return CustomerPlaceReturn;
        }

        public DTOCustomerPlaceRead GetShopsCustomerByID(int ID)
        {
            var ShopsFrCustomer = _juhinaDB.CustomerPlaces.Where(Cp=>Cp.Id==ID).Include(CP => CP.Customer).ThenInclude(C => C.Person).Include(CP => CP.Place).FirstOrDefault();
            if (ShopsFrCustomer == null)
                return null;
            else
                return _mapper.Map<DTOCustomerPlaceRead>(ShopsFrCustomer);
        }
        public List<DTOCustomerPlaceRead> GetShopsByCistomerID(int CustomerID)
        {
            var ShopsFrCustomer = _juhinaDB.CustomerPlaces.Where(Cp => Cp.Customer.ID == CustomerID).Include(CP => CP.Customer).ThenInclude(C => C.Person).Include(CP => CP.Place).ToList();
            if (ShopsFrCustomer == null)
                return null;

                return _mapper.Map<List<DTOCustomerPlaceRead>>(ShopsFrCustomer);
        }
        public bool DeleteShopsCustomerby(int ID,string Email)
        {
            var ShopsFrCustomer = _juhinaDB.CustomerPlaces.Where(Cp => Cp.Id == ID&&Cp.Customer.Person.Email==Email).FirstOrDefault();
            if (ShopsFrCustomer == null)
                return false;
            else
               { 
                _juhinaDB.Remove(ShopsFrCustomer);
                _juhinaDB.SaveChanges();
                return true;
            }
        }
        public DTOCustomerPlacereAdd AddPlaceToCustomer(DTOCustomerPlaceAdd dTOSaleCustomerAdd)
        {
            var CustomerPlace = new Models.CustomerPlace()
            {
                CustomerID=dTOSaleCustomerAdd.CustomerID,
                Place=new Place()
                {
                    Name=dTOSaleCustomerAdd.PlaceName,
                    Address=dTOSaleCustomerAdd.PlaceAddress,
                }
            };
            _juhinaDB.Add(CustomerPlace);
            _juhinaDB.SaveChanges();
            return _mapper.Map<DTOCustomerPlacereAdd>(CustomerPlace);

        }
    }
}
