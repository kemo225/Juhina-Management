using AutoMapper;
using Juhyna_DAL.Models;
using Juhyna_DAL.SaleCustomer.DTO;
using Juhyna_DAL.SaleCustomer.InterFace;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.SaleCustomer.DataAccessSaleCustomer
{
    public class ClsDataAccessSaleCustomer : ISaleCustomer
    {
        private readonly JuhinaDBContext _JuhinaDB;
        private readonly IMapper _Mapper;
        public ClsDataAccessSaleCustomer(JuhinaDBContext JuhinaDB, IMapper mapper)
        {
            _Mapper = mapper;
            _JuhinaDB = JuhinaDB;
        }

        public List<DtoSaleCustomerRead> GetSaleRelatedCustomer()
        {
            List<DtoSaleCustomerRead> dtoSaleCustomers = new List<DtoSaleCustomerRead>();
            var SaleCustmers = _JuhinaDB.SalesCustomers.AsNoTracking().Include(s => s.Customer).ThenInclude(C => C.Person)
                .Include(S => S.Sale).ThenInclude(s => s.Person).
                Include(s => s.Customer).ThenInclude(C => C.CustomerPlaces).ThenInclude(CP => CP.Place)
                .ToList();
            foreach (var s in SaleCustmers)
            {
                dtoSaleCustomers.Add(_Mapper.Map<DtoSaleCustomerRead>(s));
            }
            return dtoSaleCustomers;
        }
        public List<DtoSaleCustomerRead> GetCustomerRelatedBySaleID(int ID)
        {
            List<DtoSaleCustomerRead> dtoSaleCustomers = new List<DtoSaleCustomerRead>();
            var SaleCustmers = _JuhinaDB.SalesCustomers.Where(SC => SC.SaleID == ID).Include(s => s.Customer).ThenInclude(C => C.Person)
                .Include(S => S.Sale).ThenInclude(s => s.Person).
                Include(s => s.Customer).ThenInclude(C => C.CustomerPlaces).ThenInclude(CP => CP.Place)
                .ToList();
            foreach (var s in SaleCustmers)
            {
                dtoSaleCustomers.Add(_Mapper.Map<DtoSaleCustomerRead>(s));
            }
            return dtoSaleCustomers;
        }
        public DtoSaleCustomerRead GetSaleCustomerByID(int ID)
        {
            var SaleCustmers = _JuhinaDB.SalesCustomers.Where(SC => SC.ID == ID).Include(s => s.Customer).ThenInclude(C => C.Person)
                .Include(S => S.Sale).ThenInclude(s => s.Person).
                Include(s => s.Customer).ThenInclude(C => C.CustomerPlaces).ThenInclude(CP => CP.Place)
                .FirstOrDefault();
            var dtoSaleCustomer = _Mapper.Map<DtoSaleCustomerRead>(SaleCustmers);
            if (dtoSaleCustomer != null)
                return dtoSaleCustomer;
            else
                return null;
        }
        public List<DtoSaleCustomerRead> GetCustomerSRelatedBySaleName(string FirstName)
        {
            List<DtoSaleCustomerRead> dtoSaleCustomers = new List<DtoSaleCustomerRead>();
            var SaleCustmers = _JuhinaDB.SalesCustomers.AsNoTracking().Where(SC => SC.Sale.Person.FirstName.ToLower() == FirstName.ToLower()).Include(s => s.Customer).ThenInclude(C => C.Person)
                .Include(S => S.Sale).ThenInclude(s => s.Person).
                Include(s => s.Customer).ThenInclude(C => C.CustomerPlaces).ThenInclude(CP => CP.Place)
                .ToList();
            foreach (var s in SaleCustmers)
            {
                dtoSaleCustomers.Add(_Mapper.Map<DtoSaleCustomerRead>(s));
            }
            return dtoSaleCustomers;
        }
        public List<DtoSaleCustomerRead> GetSalesRelatedByCustomerName(string FirstName)
        {
            List<DtoSaleCustomerRead> dtoSaleCustomers = new List<DtoSaleCustomerRead>();
            var SaleCustmers = _JuhinaDB.SalesCustomers.AsNoTracking().Where(SC => SC.Customer.Person.FirstName.ToLower() == FirstName.ToLower()).Include(s => s.Customer).ThenInclude(C => C.Person)
                .Include(S => S.Sale).ThenInclude(s => s.Person).
                Include(s => s.Customer).ThenInclude(C => C.CustomerPlaces).ThenInclude(CP => CP.Place)
                .ToList();
            foreach (var s in SaleCustmers)
            {
                dtoSaleCustomers.Add(_Mapper.Map<DtoSaleCustomerRead>(s));
            }
            return dtoSaleCustomers;
        }
        public List<DtoSaleCustomerRead> GetSalesRelatedByCustomerID(int ID)
        {
            List<DtoSaleCustomerRead> dtoSaleCustomers = new List<DtoSaleCustomerRead>();
            var SaleCustmers = _JuhinaDB.SalesCustomers.AsNoTracking().Where(SC => SC.CustomerID == ID).Include(s => s.Customer).ThenInclude(C => C.Person)
                .Include(S => S.Sale).ThenInclude(s => s.Person).
                Include(s => s.Customer).ThenInclude(C => C.CustomerPlaces).ThenInclude(CP => CP.Place)
                .ToList();
            foreach (var s in SaleCustmers)
            {
                dtoSaleCustomers.Add(_Mapper.Map<DtoSaleCustomerRead>(s));
            }
            return dtoSaleCustomers;
        }
        public bool DeleteSaleCustomer(int ID)
        {
            var salecustomer = _JuhinaDB.SalesCustomers.Find(ID);
            if (salecustomer == null)
                return false;
            else
            {
                _JuhinaDB.Remove(salecustomer);
                _JuhinaDB.SaveChanges();
                return true;
            }
        }
        public DTOSaleCustomerUpdate AddSaleCustomer(DTOSaleCustomerAdd SaleCustomer)
        {
            var salecustome = new SalesCustomer()
            {
                SaleID = SaleCustomer.SaleID,
                CustomerID = SaleCustomer.CustomerID,
            };
            _JuhinaDB.Add(salecustome);
            _JuhinaDB.SaveChanges();
            return _Mapper.Map<DTOSaleCustomerUpdate>(salecustome);

        }
        public DTOSaleCustomerUpdate UpdateSaleCustomer(DTOSaleCustomerUpdate SaleCustomerUpdate)
        {
            var salecustomer = _JuhinaDB.SalesCustomers.Find(SaleCustomerUpdate.ID);
            if (salecustomer == null)
                return null;
            else
            {
                salecustomer.SaleID = SaleCustomerUpdate.SaleID;
                salecustomer.CustomerID = SaleCustomerUpdate.CustomerID;
                _JuhinaDB.SaveChanges();
                return _Mapper.Map<DTOSaleCustomerUpdate>(salecustomer);
            }
        }
    }
}
