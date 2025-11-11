using AutoMapper;
using Juhyna_DAL.FollowingSaleAdminstrative.DatacAccessFolllowingsaleAdminstrative;
using Juhyna_DAL.FollowingSaleAdminstrative.InterFace;
using Juhyna_DAL.ManageProducts.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.Orders.DTO;
using Juhyna_DAL.Orders.InterFace;
using Juhyna_DAL.Visits.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Orders.DataAccess
{
    public class ClsDataAccessOrder:IOrderDAL
    {
        private readonly JuhinaDBContext _JuhinaDB;
        private readonly IMapper _mapper;
        private readonly IInvoiceDAL _Invoice;
        private readonly IProductInventoryDAL _ProductInventory;

        public ClsDataAccessOrder(IMapper mapper, JuhinaDBContext JuhinaDB, IInvoiceDAL Invoice, IProductInventoryDAL ProductInventory)
        {
            _mapper = mapper;
            _JuhinaDB = JuhinaDB;
            _ProductInventory = ProductInventory;
            _Invoice = Invoice;
        }

        public List<DtoOrderRead> GetAllOrder()
        {
            var Orders = _JuhinaDB.Orders.AsNoTracking().Include(O => O.Visit).
                ThenInclude(V => V.Customer).ThenInclude(C => C.Person).
                Include(O => O.Visit).ThenInclude(V => V.CreatedBySale).
                ThenInclude(C => C.Person).Include(O => O.Visit)
                .ThenInclude(V => V.Place).Include(O => O.Invoice).
                ThenInclude(I => I.ProductinInventory)
                .ThenInclude(P => P.Product).Include(O => O.PaymentMethod).ToList();
            if (Orders == null || Orders.Count <= 0) { return null; }

            return _mapper.Map<List<DtoOrderRead>>(Orders);


        }
     
        public List<DtoOrderRead> GetAllOrderCompleted()
        {
            var Orders = _JuhinaDB.Orders.AsNoTracking().Include(O => O.Visit).
                ThenInclude(V => V.Customer).ThenInclude(C => C.Person).
                Include(O => O.Visit).ThenInclude(V => V.CreatedBySale).
                ThenInclude(C => C.Person).Include(O => O.Visit)
                .ThenInclude(V => V.Place).Include(O => O.Invoice).
                ThenInclude(I => I.ProductinInventory)
                .ThenInclude(P => P.Product).Include(O => O.PaymentMethod).Where(O => O.Status == (int)enStatusOrder.Completed).ToList();
            if (Orders == null || Orders.Count <= 0) { return null; }

            return _mapper.Map<List<DtoOrderRead>>(Orders);


        }
        public List<DtoOrderRead> GetAllOrderCanceled()
        {
            var Orders = _JuhinaDB.Orders.AsNoTracking().Include(O => O.Visit).
                ThenInclude(V => V.Customer).ThenInclude(C => C.Person).
                Include(O => O.Visit).ThenInclude(V => V.CreatedBySale).
                ThenInclude(C => C.Person).Include(O => O.Visit)
                .ThenInclude(V => V.Place).Include(O => O.Invoice).
                ThenInclude(I => I.ProductinInventory)
                .ThenInclude(P => P.Product).Include(O => O.PaymentMethod).Where(O => O.Status == (int)enStatusOrder.Canceled).ToList();
            if (Orders == null || Orders.Count <= 0) { return null; }

            return _mapper.Map<List<DtoOrderRead>>(Orders);


        }
        public DtoOrderRead GetOrderById(int ID)
        {
            var Orders = _JuhinaDB.Orders.Include(O => O.Visit).
                ThenInclude(V => V.Customer).ThenInclude(C => C.Person).
                Include(O => O.Visit).ThenInclude(V => V.CreatedBySale).
                ThenInclude(C => C.Person).Include(O => O.Visit).ThenInclude(V => V.Place).Include(O => O.Invoice).
                ThenInclude(I => I.ProductinInventory).ThenInclude(P => P.Product).Include(O => O.PaymentMethod).Where(O => O.ID == ID).FirstOrDefault();
            if (Orders == null) { return null; }

            return _mapper.Map<DtoOrderRead>(Orders);

        }
        private int GetTotalPrice(int Quantity, int ProductPrice)
        {
            // Calculate Taxis If Needed Here
            //
            //
            return Quantity * ProductPrice;
        }
        public DtoOrderRead AddOrder(DtoOrderAdd OrderAdd)
        {
            var Order = new Order();

            var Visit = _JuhinaDB.Visits.Find(OrderAdd.VisitID);
            var Invoice = _JuhinaDB.InvoicebetweenSaleAdminstratives.Include(I => I.ProductinInventory).
                ThenInclude(PI => PI.Product).Where(I => I.ID == OrderAdd.InvoiceID)
                .FirstOrDefault();

            if (Invoice == null || Visit == null || Invoice.ProductinInventory == null || Invoice.ProductinInventory.Product == null)
                return null;

            using (var Trans = _JuhinaDB.Database.BeginTransaction())
            {
                try
                {
                     Order = new Order()
                    {
                        Invoice = Invoice,
                        InvoiceID = OrderAdd.InvoiceID,
                        VisitID = OrderAdd.VisitID,
                        TotalPrice = GetTotalPrice(OrderAdd.Quantity, (int)Invoice.ProductinInventory.Product.Price),
                        Quantity = OrderAdd.Quantity,
                        CreatedBySaleID = OrderAdd.CreatedBySaleID,
                        Status = (int)enStatusOrder.Completed,
                        CreateAt = DateTime.Now,
                        PaymentMethodID = OrderAdd.PaymentmethodID,
                        ProductPrice = (int)Invoice.ProductinInventory.Product.Price
                    };

                    Visit.Status = (int)enStatusVisit.Bought;

                    _Invoice.ExChangeformInvoice(OrderAdd.InvoiceID, OrderAdd.Quantity);
                    _ProductInventory.ExchangefromProductininventory(Invoice.ProductinInventory.ID, OrderAdd.Quantity);
                    _JuhinaDB.Add(Order);

                    _JuhinaDB.SaveChanges();

                    Trans.Commit();
                }
                catch (Exception)
                {
                    Trans.Rollback();
                    return null;
                }
            }
            return GetOrderById(Order.ID);
            
        }
        public DToOrderUpdate UpdateOrder(DToOrderUpdate order)
        {

            var Order = _JuhinaDB.Orders.Find(order.ID);
            if (Order == null)
                return null;
            
            Order.InvoiceID = order.InvoiceID;
            Order.VisitID = order.VisitID;
            Order.CreatedBySaleID = order.CreatedBySaleID;
            Order.PaymentMethodID = order.PaymentmethodID;
            Order.Quantity = order.Quantity;
            _JuhinaDB.SaveChanges();
            return _mapper.Map<DToOrderUpdate>(Order);
        }
        public bool DeleteOrder(int ID,int InvoiceID)
        {
            var Orderr=_JuhinaDB.Orders.Include(O=>O.Invoice).ThenInclude(I=>I.ProductinInventory).Include(O=>O.Visit).Where(O=>O.ID==ID&&O.InvoiceID==InvoiceID).FirstOrDefault();
            if (Orderr == null) return false;

            using (var Trans = _JuhinaDB.Database.BeginTransaction())
            {
                try
                {
                    Orderr.Status = (int)enStatusOrder.Canceled;
                    _Invoice.ReturnExchangeformInvoice(InvoiceID, Orderr.Quantity);
                    _ProductInventory.FillfromProductininventory(Orderr.Invoice.ProductinInventory.ID, Orderr.Quantity);
                    _JuhinaDB.SaveChanges();
                    Trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    Trans.Rollback();
                    return false;
                }
            }

        }
    }
}
