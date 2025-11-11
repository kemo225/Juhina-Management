using AutoMapper;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Customer.DTO;
using Juhyna_DAL.Customer.InterFace;
using Juhyna_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Customer.DataAccessCustmer
{
    public class ClsDataAccessCustomer : ICustomer
    {
        private readonly IMapper _mapper;
        private readonly JuhinaDBContext _DBJuhina;
        public ClsDataAccessCustomer(IMapper mapper, JuhinaDBContext DBJuhina)
        {
            _mapper = mapper;
            _DBJuhina = DBJuhina;
        }
        public List<DTOCustmerRead> GetCustomers()
        {
            List<DTOCustmerRead> ReCustmers = new List<DTOCustmerRead>();

            var customers = _DBJuhina.Customers.Include(ad => ad.Person).AsNoTracking().ToList();
            foreach (var Customer in customers)
            {
                ReCustmers.Add(_mapper.Map<DTOCustmerRead>(Customer));
            }
            return ReCustmers;

        }
        public DTOCustmerRead GetCustomerReadBYID(int ID)
        {
            var Customers = _DBJuhina.Customers.Include(ad => ad.Person).FirstOrDefault(ad => ad.ID == ID);
            if (Customers != null)
                return _mapper.Map<DTOCustmerRead>(Customers);
            return null;
        }
        public DtoCustmerUpdate AddCustomer(DTOCustmerAdd Customer)
        {
            var Customeradd = new Models.Customer()
            {
                Person =
                new Person()
                {
                    FirstName = Customer.FirstName,
                    SecondName = Customer.SecondName,
                    ThirdName = Customer.ThirdName,
                    LastName = Customer.LastName,
                    Phone = Customer.Phone,
                    Email = Customer.Email,
                    Gender = (Customer.Gender.ToLower() == "male") ? 1 : 0,
                }
            };
            _DBJuhina.Add(Customeradd);
            _DBJuhina.SaveChanges();
            var CustomerRe = _mapper.Map<DtoCustmerUpdate>(Customeradd);
            if (CustomerRe.ID > 0)
                return CustomerRe;
            return null;
        }
        public bool DeleteCustomer(int ID)
        {
            var Customers = _DBJuhina.Customers.Include(a => a.Person).Where(a => a.ID == ID).FirstOrDefault();
            if (Customers != null)
            {
                _DBJuhina.Remove(Customers);
                _DBJuhina.Remove(Customers.Person);
                _DBJuhina.SaveChanges();
                return true;
            }
            return false;
        }
        public bool IsCustomerExist(int ID)
        {
            var Customer = _DBJuhina.Customers.Find(ID);
            if (Customer != null)
            {
                return true;
            }
            return false;
        }
        public DtoCustmerUpdate UpdateCustomer(DtoCustmerUpdate Customer)
        {
            var Customerup = _DBJuhina.Customers.Find(Customer.ID);
            if (Customerup != null)
            {
                Customerup.Person.FirstName = Customer.FirstName;
                Customerup.Person.LastName = Customer.LastName;
                Customerup.Person.ThirdName = Customer.ThirdName;
                Customerup.Person.Email = Customer.Email;
                Customerup.Person.Phone = Customer.Phone;
                Customerup.Person.SecondName = Customer.SecondName;
                if (Customer.Gender.ToLower() == "male")
                    Customerup.Person.Gender = 1;
                else
                    Customerup.Person.Gender = 2;
                _DBJuhina.SaveChanges();
                var CustomerRe = _mapper.Map<DtoCustmerUpdate>(Customerup);
                return CustomerRe;
            }
            return null;
        }
    }
}
