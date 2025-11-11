using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Models;

namespace Juhyna_DAL.Admins.Mapper
{
    public class AdminProfiles : Profile
    {
        public AdminProfiles()
        {
            CreateMap<Admin, DtoAdminRead>()
                .ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
                ForMember(to => to.FirstName, from => from.MapFrom(from => from.Person.FirstName)).
                ForMember(to => to.SecondName, from => from.MapFrom(from => from.Person.SecondName)).
                ForMember(to => to.ThirdName, from => from.MapFrom(from => from.Person.ThirdName)).
                ForMember(to => to.LastName, from => from.MapFrom(from => from.Person.LastName)).
                ForMember(to => to.Email, from => from.MapFrom(from => from.Person.Email)).
                ForMember(to => to.Phone, from => from.MapFrom(from => from.Person.Phone)).
                ForMember(to => to.Gender, from => from.MapFrom(from => from.Person.Gender == 1 ? "Male" : "Female")).
                ForMember(to => to.Salary, from => from.MapFrom(from => from.Person.Salary)).
                ForMember(to => to.UserName, from => from.MapFrom(from => from.UserName));
            CreateMap<Admin, DtoAdminUpdate>()
                .ForMember(to => to.ID, from => from.MapFrom(from => from.ID))
                .ForMember(to => to.FirstName, from => from.MapFrom(from => from.Person.FirstName)).
                ForMember(to => to.SecondName, from => from.MapFrom(from => from.Person.SecondName)).
                ForMember(to => to.ThirdName, from => from.MapFrom(from => from.Person.ThirdName)).
                ForMember(to => to.LastName, from => from.MapFrom(from => from.Person.LastName)).
                ForMember(to => to.Email, from => from.MapFrom(from => from.Person.Email)).
                ForMember(to => to.Phone, from => from.MapFrom(from => from.Person.Phone)).
                ForMember(to => to.Gender, from => from.MapFrom(from => from.Person.Gender == 1 ? "Male" : "Female")).
                ForMember(to => to.Salary, from => from.MapFrom(from => from.Person.Salary)).
                ForMember(to => to.UsertName, from => from.MapFrom(from => from.UserName));


        }
    }
}
