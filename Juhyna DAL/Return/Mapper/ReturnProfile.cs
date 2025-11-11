using AutoMapper;
using Juhyna_DAL.ManageProducts.DTO;
using Juhyna_DAL.Return.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Return.Mapper
{
    public class ReturnProfile:Profile
    {
        public ReturnProfile()
        {
            CreateMap<Models.Return, DtoReturnRead>().
                ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
               ForMember(to => to.AdminstrativeName, from => from.MapFrom(from => from.Adminstrative.Person.FirstName + " " + from.Adminstrative.Person.LastName)).
               ForMember(to => to.CraetedBySaleName, from => from.MapFrom(from => from.CreatedBySale.Person.FirstName + " " + from.CreatedBySale.Person.LastName)).
               ForMember(to => to.Back, from => from.MapFrom(from => from.Back)).
               ForMember(to => to.ProductName, from => from.MapFrom(from => from.Invoice.ProductinInventory.Product.Description))
               .ForMember(to => to.QuantityTaked, from => from.MapFrom(from => from.Invoice.Quantity)).
                ForMember(to => to.IsConfirmed, from => from.MapFrom(from => from.IsConfirmed)).
                ForMember(to => to.ConfirmedAt, from => from.MapFrom(from => DateOnly.FromDateTime( from.ConfirmedAt))).
                ForMember(to => to.CreatedAt, from => from.MapFrom(from => from.CreateAt));

            CreateMap<Models.Return, DtoReturnAddBySaleReturned>().
              ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
             ForMember(to => to.FolloingSaleMangerId, from => from.MapFrom(from => from.InvoiceID)).
             ForMember(to => to.CraetedBySaleId, from => from.MapFrom(from => from.CreatedBySaleID)).
             ForMember(to => to.CreatedAt, from => from.MapFrom(from => from.CreateAt)).
             ForMember(to => to.AdminstrativeId, from => from.MapFrom(from => from.AdminstrativeID))
             .ForMember(to => to.Back, from => from.MapFrom(from => from.Back));

        }
    }
}
