using AutoMapper;
using Juhyna_DAL.Models;
using Juhyna_DAL.Sales.DtoSales;
using Juhyna_DAL.Visits.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Visits.Mapper
{
    public class VisitProfile:Profile
    {
        private string GetStatus(int Status)
        {
            if (Status == Convert.ToInt32(enStatusVisit.UnInterest))
                return "UnInterest";
            else if (Status == Convert.ToInt32(enStatusVisit.Interest))
                return "Interest";
            else if (Status == Convert.ToInt32(enStatusVisit.Bought))
                return "Bought";
            else
                return "Completely";
        }
        public VisitProfile
            () {
            CreateMap<Visit, DtoVisitRead>().ForMember(to => to.Id, from => from.MapFrom(from => from.ID)).
                   ForMember(to => to.CraetedBySaleName, from => from.MapFrom(from => from.CreatedBySale.Person.FirstName + " " + from.CreatedBySale.Person.LastName)).
                   ForMember(to => to.CustomerName, from => from.MapFrom(from => from.Customer.Person.FirstName + " " + from.Customer.Person.LastName)).
                   ForMember(to => to.CreateAt, from => from.MapFrom(from => from.CreateAt)).
                   ForMember(to => to.Status, from => from.MapFrom(from => from.Status)).
                     ForMember(to => to.PhonCustomer, from => from.MapFrom(from => from.Customer.Person.Phone)).
                       ForMember(to => to.LocationCustomer, from => from.MapFrom(from => from.Place.Address)).
                       ForMember(to => to.Status, from => from.MapFrom(from =>GetStatus(Convert.ToInt32(from.Status))));


            CreateMap<Visit, DtoVisitUpdate>().ForMember(to => to.Id, from => from.MapFrom(from => from.ID)).
               ForMember(to => to.CustomerID, from => from.MapFrom(from => from.CustomerID)).
                 ForMember(to => to.CraetedBySaleID, from => from.MapFrom(from => from.CreatedBySaleID)).
                  ForMember(to => to.Status, from => from.MapFrom(from => from.Status));






        }
    }
}
