
using AutoMapper;
using Juhyna_DAL.Customer.DTO;
using Juhyna_DAL.CustomerPlace.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.CustomerPlace.Mapper
{
    public class CustomerPlaceProfile:Profile
    {
        public CustomerPlaceProfile() {
            CreateMap<Models.CustomerPlace, DTOCustomerPlaceRead>().ForMember(to => to.ID, from => from.MapFrom(from => from.Id)).
                    ForMember(to => to.CustomerName, from => from.MapFrom(from => (from.Customer.Person.FirstName + " " + from.Customer.Person.LastName))).
                    ForMember(to => to.CustomerPhone, from => from.MapFrom(from => from.Customer.Person.Phone)).
                    ForMember(to => to.PlaceName, from => from.MapFrom(from => from.Place.Name)).
                    ForMember(to => to.PlaceAddress, from => from.MapFrom(from => from.Place.Address))
                                     .   ForMember(to => to.PlaceID, from => from.MapFrom(from => from.Place.Id))




                    ;

            CreateMap<Models.CustomerPlace, DTOCustomerPlacereAdd>().ForMember(to => to.ID, from => from.MapFrom(from => from.Id)).
               ForMember(to => to.PlaceName, from => from.MapFrom(from => from.Place.Name)).
               ForMember(to => to.PlaceAddress, from => from.MapFrom(from => from.Place.Address));



        }
    }
}
