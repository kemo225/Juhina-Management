using AutoMapper;
using Juhyna_DAL.Customer.DTO;
using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.InvoiceSaleAdminstrative.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.FollowingSaleAdminstrative.Mapper
{
    public class FoloowingAdminstrativeSaleProfile:Profile
    {
        public FoloowingAdminstrativeSaleProfile()
        {
            CreateMap<Models.InvoicebetweenSaleAdminstrative, DTOFollowingSaleAdminstrativeRead>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
                     ForMember(to => to.ProductName, from => from.MapFrom(from => from.ProductinInventory.Product.Description)).
                     ForMember(to => to.Quantity, from => from.MapFrom(from => from.Quantity)).
                     ForMember(to => to.Categorey, from => from.MapFrom(from => from.ProductinInventory.Product.Category.Name)).
                     ForMember(to => to.CreatedByAdminstrativeName, from => from.MapFrom(from => (from.CreatedByAdminstrative.Person.FirstName + " " + from.CreatedByAdminstrative.Person.LastName))).
                     ForMember(to => to.SaleName, from => from.MapFrom(from => (from.Sale.Person.FirstName + " " + from.Sale.Person.LastName))).
                     ForMember(to => to.CreatedAt, from => from.MapFrom(from => from.CraeteAt)).
                     ForMember(to => to.IsConfrmed, from => from.MapFrom(from => from.IsConfirmed)).
                     ForMember(to => to.ConfirmedAt, from => from.MapFrom(from => from.ConfirmedAt)).
                       ForMember(to => to.Bought, from => from.MapFrom(from => from.Bought)).
                     ForMember(to => to.Rest, from => from.MapFrom(from => from.Rest)).
                     ForMember(to => to.Size, from => from.MapFrom(from => from.ProductinInventory.Product.Size))
                      . ForMember(to => to.EmailAdminstrative, from => from.MapFrom(from => from.CreatedByAdminstrative.Person.Email))

                     ;

            CreateMap<Models.InvoicebetweenSaleAdminstrative, DtoFollowingSaleAdminstrativeAddforadminstrative>().
                 ForMember(to => to.Quantity, from => from.MapFrom(from => from.Quantity)).
                 ForMember(to => to.ProductinInventoryID, from => from.MapFrom(from => from.ProductinInventory.Product)).
                 ForMember(to => to.SaleID, from => from.MapFrom(from => from.SaleID)).
                 ForMember(to => to.CreatedByAdminstrativeID, from => from.MapFrom(from => from.CreatedByAdminstrativeID)).
                 ForMember(to => to.CreateAt, from => from.MapFrom(from => from.CraeteAt));

            CreateMap<Models.InvoicebetweenSaleAdminstrative, DtoFollowingSaleAdminstrarive>().ForMember(to => to.Id, from => from.MapFrom(from => from.ID)).
                  ForMember(to => to.productinInventoryID, from => from.MapFrom(from => from.ProductinInventoryID)).
                  ForMember(to => to.Quantity, from => from.MapFrom(from => from.Quantity)).
                  ForMember(to => to.CreatedByAdminID, from => from.MapFrom(from => from.CreatedByAdminstrativeID)).
                  ForMember(to => to.SaleID, from => from.MapFrom(from => (from.SaleID))).
                      ForMember(to => to.Bought, from => from.MapFrom(from => from.Bought)).
                     ForMember(to => to.Rest, from => from.MapFrom(from => from.Rest)).
                     ForMember(to => to.IsConfirmed, from => from.MapFrom(from => from.IsConfirmed)).
                  ForMember(to => to.ConfirmedAt, from => from.MapFrom(from => from.ConfirmedAt)).
                    ForMember(to => to.CreateAt, from => from.MapFrom(from => from.CraeteAt));

            CreateMap<Models.InvoicebetweenSaleAdminstrative, DtoFollowingSaleAdminstrativeAddforadminstrativeReturned>().
     ForMember(to => to.Quantity, from => from.MapFrom(from => from.Quantity)).
     ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
     ForMember(to => to.ProductinInventoryID, from => from.MapFrom(from => from.ProductinInventoryID)).
     ForMember(to => to.SaleID, from => from.MapFrom(from => from.SaleID)).
     ForMember(to => to.CreatedByAdminstartiveID, from => from.MapFrom(from => from.CreatedByAdminstrativeID)).
     ForMember(to => to.CreateAt, from => from.MapFrom(from => new DateTime(from.CraeteAt.Year, from.CraeteAt.Month, from.CraeteAt.Day)))
         .ForMember(to => to.productprice, from => from.MapFrom(from => from.ProductinInventory.Product.Price)).
     ForMember(to => to.ProductName, from => from.MapFrom(from => from.ProductinInventory.Product.Description)).
     ForMember(to => to.InventoryName, from => from.MapFrom(from => from.ProductinInventory.Inventory.Name))
     .ForMember(to => to.InventoryAddress, from => from.MapFrom(from => from.ProductinInventory.Inventory.Address))
          .ForMember(to => to.CreatedByAdminstrativeName, from => from.MapFrom(from => from.CreatedByAdminstrative.Person.FirstName+" "+ from.CreatedByAdminstrative.Person.FirstName))


     ;








        }
    }
}
