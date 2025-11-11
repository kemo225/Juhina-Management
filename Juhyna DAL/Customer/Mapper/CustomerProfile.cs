using AutoMapper;
using Juhyna_DAL.Customer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Customer.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Models.Customer, DTOCustmerRead>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
                    ForMember(to => to.FirstName, from => from.MapFrom(from => from.Person.FirstName)).
                    ForMember(to => to.SecondName, from => from.MapFrom(from => from.Person.SecondName)).
                    ForMember(to => to.ThirdName, from => from.MapFrom(from => from.Person.ThirdName)).
                    ForMember(to => to.LastName, from => from.MapFrom(from => from.Person.LastName)).
                    ForMember(to => to.Email, from => from.MapFrom(from => from.Person.Email)).
                    ForMember(to => to.Phone, from => from.MapFrom(from => from.Person.Phone)).
                    ForMember(to => to.Gender, from => from.MapFrom(from => from.Person.Gender == 1 ? "Male" : "Female"));
            CreateMap<Models.Customer, DtoCustmerUpdate>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID))
           .ForMember(to => to.FirstName, from => from.MapFrom(from => from.Person.FirstName)).
           ForMember(to => to.SecondName, from => from.MapFrom(from => from.Person.SecondName)).
           ForMember(to => to.ThirdName, from => from.MapFrom(from => from.Person.ThirdName)).
           ForMember(to => to.LastName, from => from.MapFrom(from => from.Person.LastName)).
           ForMember(to => to.Email, from => from.MapFrom(from => from.Person.Email)).
           ForMember(to => to.Phone, from => from.MapFrom(from => from.Person.Phone)).
           ForMember(to => to.Gender, from => from.MapFrom(from => from.Person.Gender == 1 ? "Male" : "Female"));
        }
    }
}
