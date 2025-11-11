using AutoMapper;
using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.FollowingSaleAdminstrative.InterFace;
using Juhyna_DAL.InvoiceSaleAdminstrative.DTO;
using Juhyna_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.FollowingSaleAdminstrative.DatacAccessFolllowingsaleAdminstrative
{
    public enum enStatusInvoice
    {
        Waiting = 0,Confirmed= 1,Canceled=2,Completed=3
    }
    public class ClsDatacAccessInvoices : IInvoiceDAL
    {
        private readonly IMapper _mapper;
        private readonly JuhinaDBContext _DBJuhina;
        public ClsDatacAccessInvoices(IMapper mapper, JuhinaDBContext DBJuhina)
        {
            _mapper = mapper;
            _DBJuhina = DBJuhina;
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrative()
        {
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.Include(F => F.Sale).
                ThenInclude(S => S.Person).Include(F => F.CreatedByAdminstrative)
                .ThenInclude(C => C.Person).Include(F => F.ProductinInventory).ThenInclude(p=>p.Product)
                .ThenInclude(P => P.Category).AsNoTracking().ToList();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
          
              return _mapper.Map<List<DTOFollowingSaleAdminstrativeRead>>(InvoiceBetweenSaleAdminstrative);
            
        }
        public DTOFollowingSaleAdminstrativeRead GetInvoiceBetweenSaleAdminstrativeDetailsByID(int ID)
        {
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.Where(F => F.ID == ID).Include(F => F.Sale).ThenInclude(S => S.Person)
                .Include(F => F.CreatedByAdminstrative).ThenInclude(C => C.Person).
                Include(F => F.ProductinInventory)
                .ThenInclude(p => p.Product).ThenInclude(P => P.Category)
                .AsNoTracking().FirstOrDefault();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
          
                return _mapper.Map<DTOFollowingSaleAdminstrativeRead>(InvoiceBetweenSaleAdminstrative);
            
        }

        public DtoFollowingSaleAdminstrarive GetInvoiceBetweenSaleAdminstrativeByID(int ID)
        {
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.Where(F => F.ID == ID).Include(F => F.Sale).ThenInclude(S => S.Person)
                .Include(F => F.CreatedByAdminstrative).ThenInclude(C => C.Person).
                Include(F => F.ProductinInventory)
                .ThenInclude(p => p.Product).ThenInclude(P => P.Category)
                .AsNoTracking().FirstOrDefault();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
            
                return _mapper.Map<DtoFollowingSaleAdminstrarive>(InvoiceBetweenSaleAdminstrative);
            
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeToday()
        {
            var InvoiceBetweenSaleAdminstrativereturn = new List<DTOFollowingSaleAdminstrativeRead>();
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.Where(F => F.CraeteAt == DateOnly.FromDateTime(DateTime.Now)).Include(F => F.Sale).ThenInclude(S => S.Person)
                .Include(F => F.CreatedByAdminstrative).ThenInclude(C => C.Person).
                Include(F => F.ProductinInventory)
                .ThenInclude(p => p.Product)
                .ThenInclude(P => P.Category).AsNoTracking().ToList();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
           return _mapper.Map<List<DTOFollowingSaleAdminstrativeRead>>(InvoiceBetweenSaleAdminstrative);
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeAt(string Date)
        {
            var InvoiceBetweenSaleAdminstrativereturn = new List<DTOFollowingSaleAdminstrativeRead>();
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.Where(F => F.CraeteAt == DateOnly.FromDateTime(DateTime.Now)).Include(F => F.Sale).ThenInclude(S => S.Person)
                .Include(F => F.CreatedByAdminstrative).ThenInclude(C => C.Person).
                Include(F => F.ProductinInventory).ThenInclude(p => p.Product)
                .ThenInclude(P => P.Category)
                .Where(f => f.CraeteAt == DateOnly.FromDateTime(Convert.ToDateTime(Date)))
                .AsNoTracking().ToList();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
            else
            {
                foreach (var In in InvoiceBetweenSaleAdminstrative)
                {
                    InvoiceBetweenSaleAdminstrativereturn.Add(_mapper.Map<DTOFollowingSaleAdminstrativeRead>(In));
                }
                return InvoiceBetweenSaleAdminstrativereturn;
            }
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeAt(string Date, int ProductId)
        {
            var InvoiceBetweenSaleAdminstrativereturn = new List<DTOFollowingSaleAdminstrativeRead>();
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.Where(F => F.CraeteAt == DateOnly.FromDateTime(DateTime.Now)).Include(F => F.Sale).ThenInclude(S => S.Person)
                .Include(F => F.CreatedByAdminstrative).ThenInclude(C => C.Person).
                Include(F => F.ProductinInventory)
                .ThenInclude(p => p.Product)
                .ThenInclude(P => P.Category)
                .AsNoTracking().Where(f => f.CraeteAt == DateOnly.FromDateTime(Convert.ToDateTime(Date)) && f.ProductinInventory.ProductID == ProductId).ToList();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
       return _mapper.Map<List<DTOFollowingSaleAdminstrativeRead>>(InvoiceBetweenSaleAdminstrative);

        }
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenSaleAdminstrtivesbySaleID(int SaleID)
        {
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.AsNoTracking().Where(F => F.SaleID == SaleID).Include(F => F.Sale).ThenInclude(S => S.Person)
                .Include(F => F.CreatedByAdminstrative).ThenInclude(C => C.Person).
                Include(F => F.ProductinInventory).ThenInclude(p => p.Product).ThenInclude(P => P.Category).ToList();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
                return _mapper.Map<List<DTOFollowingSaleAdminstrativeRead>>(InvoiceBetweenSaleAdminstrative);
            
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenSaleAdminstrtivesbySaleIDToday(int SaleID)
        {
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.AsNoTracking().Where(F => F.SaleID == SaleID&&F.CraeteAt==DateOnly.FromDateTime(DateTime.Now)).Include(F => F.Sale).ThenInclude(S => S.Person)
                .Include(F => F.CreatedByAdminstrative).ThenInclude(C => C.Person).
                Include(F => F.ProductinInventory).ThenInclude(p => p.Product).ThenInclude(P => P.Category).ToList();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
            return _mapper.Map<List<DTOFollowingSaleAdminstrativeRead>>(InvoiceBetweenSaleAdminstrative);

        }
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenSaleAdminstrtivesbyFirstName(string SaleFirstName)
        {
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.AsNoTracking().Where(F => F.Sale.Person.FirstName == SaleFirstName).Include(F => F.Sale).ThenInclude(S => S.Person)
                .Include(F => F.CreatedByAdminstrative).ThenInclude(C => C.Person).
                Include(F => F.ProductinInventory)
                .ThenInclude(p => p.Product).ThenInclude(P => P.Category).ToList();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
           
                return _mapper.Map<List<DTOFollowingSaleAdminstrativeRead>>(InvoiceBetweenSaleAdminstrative);
            
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenAdminstrativeSalesbyAdminstrativeID(int AdminstrativeID)
        {
            var InvoiceBetweenSaleAdminstrative = _DBJuhina.InvoicebetweenSaleAdminstratives.AsNoTracking().Where(F => F.CreatedByAdminstrativeID == AdminstrativeID).Include(F => F.Sale).ThenInclude(S => S.Person)
                .Include(F => F.CreatedByAdminstrative).ThenInclude(C => C.Person).
                Include(F => F.ProductinInventory).ThenInclude(p => p.Product).ThenInclude(P => P.Category).ToList();
            if (InvoiceBetweenSaleAdminstrative == null)
                return null;
            else
            {
                return _mapper.Map<List<DTOFollowingSaleAdminstrativeRead>>(InvoiceBetweenSaleAdminstrative);
            }
        }
        public bool DeleteInvoicebyID(int ID, int adminstrativeID)
        {
            var invoice = _DBJuhina.InvoicebetweenSaleAdminstratives.Include(I=>I.ProductinInventory).Where(f => f.ID == ID && f.CreatedByAdminstrativeID == adminstrativeID).FirstOrDefault();
            if (invoice == null) return false;
            else
            {
                invoice.ProductinInventory.InventoryQuantity += invoice.Quantity;
                invoice.ProductinInventory.WaitingQuantity -= invoice.Quantity;
                _DBJuhina.Remove(invoice);
                _DBJuhina.SaveChanges();
                return true;
            }
        }
        public bool UpdateInvoiceByAdminstrative(DtoFollowingSaleAdminstrativeUpdateforAdminstrative dto)
        {
            var invoice = _DBJuhina.InvoicebetweenSaleAdminstratives.Where(f => f.ID == dto.ID).FirstOrDefault();
            if (invoice==null)
                return false;
            else
            {
                invoice.SaleID = dto.SaleID;
                invoice.Quantity = dto.Quantity;
                _DBJuhina.SaveChanges();
                return true;
            }
        }
        public bool UpdateInvoiceBySale(DtoFollowingSaleAdminstrativeupdateforSale dto)
        {
            var invoice = _DBJuhina.InvoicebetweenSaleAdminstratives.Where(f => f.ID == dto.Id).FirstOrDefault();
            if (invoice == null)
                return false;
            else
            {
                invoice.IsConfirmed = dto.IsConfirm;
                _DBJuhina.SaveChanges();
                return true;
            }
        }
        public DtoFollowingSaleAdminstrativeAddforadminstrativeReturned AddInvoiceByAdminstative(DtoFollowingSaleAdminstrativeAddforadminstrative dto)
        {
            var productinventorycheck = _DBJuhina.ProductInventories.Find(dto.ProductinInventoryID);
            if (productinventorycheck == null)
                return null;
            productinventorycheck=_DBJuhina.ProductInventories.Where(p => p.ID == dto.ProductinInventoryID).Include(p=>p.Product).Include(Product=>Product.Inventory)
  .FirstOrDefault();
            var invoice = new Models.InvoicebetweenSaleAdminstrative()
            {
                ProductinInventory= productinventorycheck,
                ProductinInventoryID = dto.ProductinInventoryID,
                Quantity = dto.Quantity,
                SaleID = dto.SaleID,
                CreatedByAdminstrativeID = dto.CreatedByAdminstrativeID,
                IsConfirmed = false,
                ConfirmedAt = new DateTime(2000, 1, 1),
                CraeteAt = DateOnly.FromDateTime(DateTime.Now),
                Bought=0,
                Rest=dto.Quantity,
                Status=(int)enStatusInvoice.Waiting   /// 1: Confrimed  , 2: Canceled , 3: Completed , 0:Waiting
          ,CreatedByAdminstrative=_DBJuhina.Adminstratives.Include(a=>a.Person).Where(a=>a.ID==dto.CreatedByAdminstrativeID).FirstOrDefault()
            };
            
           
                _DBJuhina.Add(invoice);
            var productinventory = _DBJuhina.ProductInventories.Find(dto.ProductinInventoryID);
            if (productinventory == null || invoice == null)
                return null;
            productinventory.WaitingQuantity += dto.Quantity;
            productinventory.InventoryQuantity-= dto.Quantity;
            _DBJuhina.SaveChanges();
            return _mapper.Map<DtoFollowingSaleAdminstrativeAddforadminstrativeReturned>(invoice);
        }
        public bool ConfirmBySale(DtoFollowingSaleAdminstrativeAddforSale dto)
        {
            var invoice = _DBJuhina.InvoicebetweenSaleAdminstratives.Find(dto.ID);
            if (invoice == null)
                return false;
            else
            {
                invoice.IsConfirmed = dto.IsConfirm;
                invoice.ConfirmedAt = DateTime.Now;
                if (invoice.IsConfirmed)
                    invoice.Status = (int)enStatusInvoice.Confirmed;
                else
                    invoice.Status = (int)enStatusInvoice.Canceled;
                    _DBJuhina.SaveChanges();
                return true;
            }
        }

        public bool ExChangeformInvoice(int InvoiceID,int Quantity)
        {
            var Invoice = _DBJuhina.InvoicebetweenSaleAdminstratives.Find(InvoiceID);

            if (Invoice == null) return false;
            else if (Invoice.Rest < Quantity)
                return false;
            else { 
            Invoice.Bought += Quantity;
            Invoice.Rest-= Quantity;
            if (Invoice.Rest == 0)
                Invoice.Status =(int) enStatusInvoice.Completed;
                _DBJuhina.SaveChanges();
            return true;
            }
        }

        public bool ReturnExchangeformInvoice(int InvoiceID, int Quantity)
        {
            var Invoice = _DBJuhina.InvoicebetweenSaleAdminstratives.Find(InvoiceID);

            if (Invoice == null) return false;
            else
            {
                Invoice.Bought -= Quantity;
                Invoice.Rest += Quantity;
                _DBJuhina.SaveChanges();

                return true;
            }
        }

    }
}
