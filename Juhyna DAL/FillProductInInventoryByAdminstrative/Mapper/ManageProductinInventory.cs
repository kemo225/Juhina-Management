using AutoMapper;
using Juhyna_DAL.ManageProductInInventoryByAdminstrative.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.ManageProductInInventoryByAdminstrative.Mapper
{
    public class ManageProductinInventory:Profile
    {
       public ManageProductinInventory()
        {
            CreateMap<Models.FillProductinInventory, DtoManageProductWithAdminstrativeRead>().
     ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
     ForMember(to => to.ProductName, from => from.MapFrom(from => from.ProductInventory.Product.Description)).
     ForMember(to => to.FillQuantity, from => from.MapFrom(from => from.FillQuantity)).
     ForMember(to => to.CraetedByAdmimstrative, from => from.MapFrom(from => (from.Adminstrative.Person.FirstName + " " + from.Adminstrative.Person.LastName))).
     ForMember(to => to.CreateAt, from => from.MapFrom(from => from.CreateAt))
     .ForMember(to => to.InventoryName, from => from.MapFrom(from => from.ProductInventory.Inventory.Name))
        .ForMember(to => to.InventoryLocation, from => from.MapFrom(from => from.ProductInventory.Inventory.Address));
        }
    }
}
