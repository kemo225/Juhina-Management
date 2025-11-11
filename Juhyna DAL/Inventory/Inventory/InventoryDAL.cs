using AutoMapper;
using Juhyna_DAL.Inventory.Dto;
using Juhyna_DAL.Inventory.InterFace;
using Juhyna_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Inventory.Inventory
{
    public class InventoryDAL:IInventoryDAL
    {
        private readonly JuhinaDBContext _JuhinaDB;
        private readonly IMapper _mapper;
        public InventoryDAL(JuhinaDBContext JuhinaDB,IMapper mapper)
        {
            _JuhinaDB = JuhinaDB;
            _mapper = mapper;
        }
    
        public List<DtoInventoryRead> GetAllInventories()
        {
            var inventories = _JuhinaDB.Inventories.AsNoTracking().ToList();
            if (inventories == null)
                return null;

            return _mapper.Map<List<DtoInventoryRead>>(inventories);
        }

        public DtoInventoryRead GetInventoryByID(int ID)
        {
            var inventory = _JuhinaDB.Inventories.Where(I=>I.ID==ID).FirstOrDefault();
            if (inventory == null)
                return null;
            return _mapper.Map<DtoInventoryRead>(inventory);
        }
        public int AddInventory(DtoInventoryAdd inventory)
        {
            var invetoryadd = new Models.Inventory()
            {
                Name = inventory.Name,
                Address = inventory.Address

            }
            ;
            _JuhinaDB.Inventories.Add(invetoryadd);
            _JuhinaDB.SaveChanges();
            return invetoryadd.ID;
        }   
        public bool DeleteInventory(int ID)
        {
            var inventory = _JuhinaDB.Inventories.Where(I => I.ID == ID).FirstOrDefault();
            if (inventory == null)
                return false;
            _JuhinaDB.Inventories.Remove(inventory);
            _JuhinaDB.SaveChanges();
            return true;
        }


    }
}
