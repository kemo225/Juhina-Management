using Juhyna_BLL.FollowingSaleAdminstrative.Interface;
using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.FollowingSaleAdminstrative.InterFace;
using Juhyna_DAL.InvoiceSaleAdminstrative.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.FollowingSaleAdminstrative.NewFolder
{
        public class ClsInvoice:IInvoicesBLL
       {
        private readonly IInvoiceDAL _InvoiceDAL;
        public bool ReturnExchangeformInvoice(int InvoiceID, int Quantity)
        {
            return _InvoiceDAL.ReturnExchangeformInvoice(InvoiceID, Quantity);
        }
        public bool ExChangeformInvoice(int InvoiceID, int Quantity)
        {
            return _InvoiceDAL.ExChangeformInvoice(InvoiceID, Quantity);    
        }
        public ClsInvoice(IInvoiceDAL followingSaleAdminstrative)
        {
            this._InvoiceDAL = followingSaleAdminstrative;
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenAdminstrativeSalesbyAdminstrativeID(int AdminstrativeID)
        {
            return _InvoiceDAL.GetInvoiceBetweenAdminstrativeSalesbyAdminstrativeID(AdminstrativeID);
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenSaleAdminstrtivesbySaleID(int SaleID)
        {
            return _InvoiceDAL.GetInvoiceBetweenSaleAdminstrtivesbySaleID(SaleID);
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrative()
        {
        return   _InvoiceDAL.GetAllInvoiceBetweenSaleAdminstrative();
        }
        public DTOFollowingSaleAdminstrativeRead GetInvoiceBetweenSaleAdminstrativeDetailsByID(int ID)
        {
            return _InvoiceDAL.GetInvoiceBetweenSaleAdminstrativeDetailsByID(ID);
        }
        public DtoFollowingSaleAdminstrarive GetInvoiceBetweenSaleAdminstrativeByID(int ID)
        {
            return _InvoiceDAL.GetInvoiceBetweenSaleAdminstrativeByID(ID);
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenSaleAdminstrtivesbySaleIDToday(int SaleID)
        {
            return _InvoiceDAL.GetInvoiceBetweenSaleAdminstrtivesbySaleIDToday(SaleID);
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeToday()
        {
            return _InvoiceDAL.GetAllInvoiceBetweenSaleAdminstrativeToday();
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeAt(string Date, int ProductId)
        {
            return _InvoiceDAL.GetAllInvoiceBetweenSaleAdminstrativeAt(Date, ProductId);    
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeAt(string Date)
        {
            return _InvoiceDAL.GetAllInvoiceBetweenSaleAdminstrativeAt(Date);
        }
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenSaleAdminstrtivesbyFirstName(string SaleFirstName)
        {
            return _InvoiceDAL.GetInvoiceBetweenSaleAdminstrtivesbyFirstName(SaleFirstName);
        }
        public bool DeleteInvoicebyID(int ID, int adminstrativeID)
        {
            return _InvoiceDAL.DeleteInvoicebyID(ID, adminstrativeID);  
        }
        public bool UpdateInvoiceBySale(DtoFollowingSaleAdminstrativeupdateforSale dto)
        {
          return _InvoiceDAL.UpdateInvoiceBySale(dto);  
        }
        public bool UpdateInvoiceByAdminstrative(DtoFollowingSaleAdminstrativeUpdateforAdminstrative dto)
        {
            return _InvoiceDAL.UpdateInvoiceByAdminstrative(dto);
        }
        public DtoFollowingSaleAdminstrativeAddforadminstrativeReturned AddInvoiceByAdminstative(DtoFollowingSaleAdminstrativeAddforadminstrative dto)
        {
            return _InvoiceDAL.AddInvoiceByAdminstative(dto);
        }
        public bool ConfirmBySale(DtoFollowingSaleAdminstrativeAddforSale dto)
        {
            return _InvoiceDAL.ConfirmBySale(dto);   
        }

    }
}
