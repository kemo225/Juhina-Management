using AutoMapper;
using Juhyna_DAL.Inventory.Dto;
using Juhyna_DAL.Inventory.Inventory;
using Juhyna_DAL.InvoiceSaleAdminstrative.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Inventory.Mapper
{
    public class Inventorymapper:Profile
    {
        public Inventorymapper()
        {
            

            CreateMap<Models.Inventory, DtoInventoryRead>()
                .ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
          ForMember(to => to.InventoryName, from => from.MapFrom(from => from.Name)).
          ForMember(to => to.InventoryAddress, from => from.MapFrom(from => from.Address));
        }
    }
}
