using AutoMapper;
using Juhyna_DAL.Customer.DTO;
using Juhyna_DAL.Models;
using Juhyna_DAL.SaleCustomer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.SaleCustomer.Mapper
{
    public class SaleCustomerProfile : Profile
    {
        public SaleCustomerProfile()
        {
            CreateMap<SalesCustomer, DtoSaleCustomerRead>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
                ForMember(to => to.CustomerName, from => from.MapFrom(from => (from.Customer.Person.FirstName + " " + from.Customer.Person.LastName))).
                   ForMember(to => to.SaleName, from => from.MapFrom(from => (from.Sale.Person.FirstName + " " + from.Sale.Person.LastName))).
                      ForMember(to => to.CustomerPlces, from => from.MapFrom(from => from.Customer.CustomerPlaces.Select(Cp => new DtoCustomerPlceRead()
                      {
                          PlaceName = Cp.Place.Name,
                          Address = Cp.Place.Address
                      }
                     ).ToList()));
            CreateMap<SalesCustomer, DTOSaleCustomerUpdate>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
              ForMember(to => to.CustomerID, from => from.MapFrom(from => from.CustomerID)).ForMember(to => to.SaleID, from => from.MapFrom(from => from.SaleID));


        }
    }
}

