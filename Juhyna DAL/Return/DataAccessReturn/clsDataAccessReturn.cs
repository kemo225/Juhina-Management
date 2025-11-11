using AutoMapper;
using AutoMapper.QueryableExtensions;
using Juhyna_DAL.Models;
using Juhyna_DAL.Return.DTO;
using Juhyna_DAL.Return.InterFace;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Return.DataAccessReturn
{
    public class clsDataAccessReturn:IReturnDAL
    {
        private readonly JuhinaDBContext juhinaDB;
        private readonly IMapper mapper;
        public clsDataAccessReturn(JuhinaDBContext juhinaDB, IMapper mapper)
        {
            this.juhinaDB = juhinaDB;
            this.mapper = mapper;
        }
        public List<DtoReturnRead> GetAllReturns()
        {
                var allReturns = juhinaDB.Returns.AsNoTracking().Include(R => R.Adminstrative).ThenInclude(a=>a.Person).Include(R => R.CreatedBySale).ThenInclude(c => c.Person).Include(R => R.Invoice).ThenInclude(F => F.ProductinInventory).ThenInclude(R=>R.Product).ToList();
            if (allReturns == null )
                return null;
            
                return mapper.Map<List<DtoReturnRead>>(allReturns);
        }
        public List<DtoReturnRead> GetAllReturnsBySaleID(int SaleID)
        {
            var allReturns = juhinaDB.Returns.AsNoTracking().Include(R => R.Adminstrative).ThenInclude(a => a.Person).Include(R => R.CreatedBySale).ThenInclude(c => c.Person).Include(R => R.Invoice).ThenInclude(F => F.ProductinInventory).ThenInclude(R => R.Product).Where(R=>R.CreatedBySaleID==SaleID).ToList();
            if (allReturns == null)
                return null;

            return mapper.Map<List<DtoReturnRead>>(allReturns);
        }
        public List<DtoReturnRead> GetAllReturnsTodayBySaleID(int SaleID)
        {
            var allReturns = juhinaDB.Returns.AsNoTracking().Include(R => R.Adminstrative).ThenInclude(a => a.Person).Include(R => R.CreatedBySale).ThenInclude(c => c.Person).Include(R => R.Invoice).ThenInclude(F => F.ProductinInventory).ThenInclude(R => R.Product).Where(R => R.CreatedBySaleID == SaleID&&R.CreateAt==DateOnly.FromDateTime(DateTime.Now)).ToList();
            if (allReturns == null)
                return null;

            return mapper.Map<List<DtoReturnRead>>(allReturns);
        }
        public List<DtoReturnRead> GetAllUnConfirmedReturns()
        {
            var allUnConfirmedReturns = juhinaDB.Returns.AsNoTracking().Include(R => R.Adminstrative).ThenInclude(a => a.Person).Include(R => R.CreatedBySale).ThenInclude(c => c.Person).Include(R => R.Invoice).ThenInclude(F => F.ProductinInventory).ThenInclude(R => R.Product).Where(R => R.IsConfirmed == false).ToList();
            if (allUnConfirmedReturns == null)
                return null;
            else
                return mapper.Map<List<DtoReturnRead>>(allUnConfirmedReturns);
        }
        public List<DtoReturnRead> GetAllConfirmedReturns()
        {
            var allUnConfirmedReturns = juhinaDB.Returns.AsNoTracking().Include(R => R.Adminstrative).ThenInclude(a => a.Person).Include(R => R.CreatedBySale).ThenInclude(c => c.Person).Include(R => R.Invoice).ThenInclude(F => F.ProductinInventory).ThenInclude(R => R.Product).Where(R => R.IsConfirmed == true).ToList();
            if (allUnConfirmedReturns == null)
                return null;
            else
                return mapper.Map<List<DtoReturnRead>>(allUnConfirmedReturns);
        }
        public DtoReturnRead GetReturnById(int id)
        {
            var Return = juhinaDB.Returns.Include(R => R.Adminstrative).ThenInclude(a => a.Person).Include(R => R.CreatedBySale).ThenInclude(c => c.Person).Include(R => R.Invoice).ThenInclude(F => F.ProductinInventory).ThenInclude(R => R.Product).FirstOrDefault();
            if (Return == null)
                return null;
            else
                return mapper.Map<DtoReturnRead>(Return);
        }
        public DtoReturnAddBySaleReturned AddReturn(DtoReturnAddBySale dtoReturnAddBySale)
        {
            try
            {
                var Return = new Models.Return()
                {
                    AdminstrativeID = dtoReturnAddBySale.AdminstrativeId,
                    CreatedBySaleID = dtoReturnAddBySale.CraetedBySaleId,
                    Back = dtoReturnAddBySale.Back,
                    InvoiceID = dtoReturnAddBySale.InvoiceID,
                    CreateAt = DateOnly.FromDateTime(DateTime.Now),
                    IsConfirmed = false,
                    ConfirmedAt = DateTime.MinValue,
                };
              
                juhinaDB.Returns.Add(Return);
                juhinaDB.SaveChanges();
                return mapper.Map<DtoReturnAddBySaleReturned>(Return);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool IsEvaluateReturn(DtoReturnAddByAdminstrative dtoReturnAddByAdminstrative)
        {
            try
            {
                var returnToConfirm = juhinaDB.Returns.Include(R=>R.Invoice).ThenInclude(I=>I.ProductinInventory).FirstOrDefault(R => R.ID == dtoReturnAddByAdminstrative.ID);
                if (returnToConfirm == null)
                {
                    return false;
                }
                returnToConfirm.IsConfirmed = dtoReturnAddByAdminstrative.IsConfirmed;
                returnToConfirm.ConfirmedAt = DateTime.Now;
                if (returnToConfirm.IsConfirmed == false)
                    return true;
                returnToConfirm.Invoice.ProductinInventory.WaitingQuantity -= returnToConfirm.Back;
                returnToConfirm.Invoice.ProductinInventory.InventoryQuantity += returnToConfirm.Back;
                returnToConfirm.Invoice.Rest-= returnToConfirm.Back;
                returnToConfirm.Invoice.Quantity -= returnToConfirm.Back;
                juhinaDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteReturnById(int returnId)
        {
            var ReturnToDelete = juhinaDB.Returns.FirstOrDefault(R => R.ID == returnId);
            if (ReturnToDelete == null)
                return false;

                juhinaDB.Returns.Remove(ReturnToDelete);
                juhinaDB.SaveChanges();
                return true;

        }
        public bool UpdateReturnBySale(DtoReturnUpdateBySale dtoReturnUpdate)
        {
            try
            {
                var returnToUpdate = juhinaDB.Returns.FirstOrDefault(R => R.ID == dtoReturnUpdate.ID);
                if (returnToUpdate == null)
                {
                    return false;
                }
                returnToUpdate.Back = dtoReturnUpdate.Back;
                returnToUpdate.AdminstrativeID = dtoReturnUpdate.AdminstrativeId;
                returnToUpdate.CreatedBySaleID = dtoReturnUpdate.CraetedBySaleId;
                returnToUpdate.InvoiceID = dtoReturnUpdate.InvoiceId;
                juhinaDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateReturnByAdminstrative(DtoReturnUpdateByAdminstrarive dtoReturnUpdate)
        {
            try
            {
                var returnToUpdate = juhinaDB.Returns.FirstOrDefault(R => R.ID == dtoReturnUpdate.ID);
                if (returnToUpdate == null)
                {
                    return false;
                }
                returnToUpdate.IsConfirmed= dtoReturnUpdate.IsConfirmed;
                returnToUpdate.ConfirmedAt = DateTime.Now;

                juhinaDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
