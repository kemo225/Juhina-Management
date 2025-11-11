using AutoMapper;
using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.ManageProducts.DTO;
using Juhyna_DAL.ProductInventory.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProducts.mapper
{
    public class ManageProductProfile:Profile
    {
        public ManageProductProfile()
        {
            CreateMap<Models.ProductInventory, DtoProductInventoryRead>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
                  ForMember(to => to.Quantity, from => from.MapFrom(from => from.QuantityTotal)).
                  ForMember(to => to.Bought, from => from.MapFrom(from => from.Bought)).
                  ForMember(to => to.Rest, from => from.MapFrom(from => from.Rest)).
                  ForMember(to => to.ProductID, from => from.MapFrom(from => from.ProductID)).
                  ForMember(to => to.ProductName, from => from.MapFrom(from => from.Product.Description)).
                  ForMember(to => to.ProductID, from => from.MapFrom(from => from.ProductID)).
                   ForMember(to => to.WaitingQuantity, from => from.MapFrom(from => from.WaitingQuantity))
                   .ForMember(to => to.InventoryQuantity, from => from.MapFrom(from => from.InventoryQuantity));

            CreateMap<Models.ProductInventory, DtoProductInventoryAddReturned>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
               ForMember(to => to.QuantityTotal, from => from.MapFrom(from => from.QuantityTotal)).
               ForMember(to => to.bought, from => from.MapFrom(from => from.Bought)).
               ForMember(to => to.Rest, from => from.MapFrom(from => from.Rest)).
               ForMember(to => to.ProductID, from => from.MapFrom(from => from.ProductID)).
                ForMember(to => to.WaitingQuantity, from => from.MapFrom(from => from.WaitingQuantity))
                .ForMember(to => to.InventoryQuantity, from => from.MapFrom(from => from.InventoryQuantity))
                       .ForMember(to => to.InventoryID, from => from.MapFrom(from => from.InventoryID));

        }
    }
}
