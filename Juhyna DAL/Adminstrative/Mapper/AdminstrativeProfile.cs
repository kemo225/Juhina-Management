using AutoMapper;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Adminstrative.DTO;
using Juhyna_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Adminstrative.Mapper
{
    public class AdminstrativeProfile : Profile
    {
        public AdminstrativeProfile()
        {
            CreateMap<Models.Adminstrative, DTOAdminstrativeRead>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
                ForMember(to => to.FirstName, from => from.MapFrom(from => from.Person.FirstName)).
                ForMember(to => to.SecondName, from => from.MapFrom(from => from.Person.SecondName)).
                ForMember(to => to.ThirdName, from => from.MapFrom(from => from.Person.ThirdName)).
                ForMember(to => to.LastName, from => from.MapFrom(from => from.Person.LastName)).
                ForMember(to => to.Email, from => from.MapFrom(from => from.Person.Email)).
                ForMember(to => to.Phone, from => from.MapFrom(from => from.Person.Phone)).
                ForMember(to => to.Gender, from => from.MapFrom(from => (from.Person.Gender == 1) ? "Male" : "Female")).
                ForMember(to => to.Salary, from => from.MapFrom(from => from.Person.Salary)).
                ForMember(to => to.InventoryName, from => from.MapFrom(from => from.Inventory.Name)).
                ForMember(to => to.UserName, from => from.MapFrom(from => from.UserName));
            CreateMap<Models.Adminstrative, DtoAdminStrativeUpdate>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID))
           .ForMember(to => to.FirstName, from => from.MapFrom(from => from.Person.FirstName)).
           ForMember(to => to.SecondName, from => from.MapFrom(from => from.Person.SecondName)).
           ForMember(to => to.ThirdName, from => from.MapFrom(from => from.Person.ThirdName)).
           ForMember(to => to.LastName, from => from.MapFrom(from => from.Person.LastName)).
           ForMember(to => to.Email, from => from.MapFrom(from => from.Person.Email)).
           ForMember(to => to.Phone, from => from.MapFrom(from => from.Person.Phone)).
           ForMember(to => to.Gender, from => from.MapFrom(from => (from.Person.Gender == 1) ? "Male" : "Female")).
           ForMember(to => to.Salary, from => from.MapFrom(from => from.Person.Salary)).
           ForMember(to => to.UsertName, from => from.MapFrom(from => from.UserName)).
                ForMember(to => to.InvrnotyID, from => from.MapFrom(from => from.InventoryID))
           ;
        }
    }
}
