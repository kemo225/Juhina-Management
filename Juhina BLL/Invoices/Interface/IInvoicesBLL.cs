using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.FollowingSaleAdminstrative.InterFace;
using Juhyna_DAL.InvoiceSaleAdminstrative.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.FollowingSaleAdminstrative.Interface
{
    public interface IInvoicesBLL

    {
        public bool ReturnExchangeformInvoice(int InvoiceID, int Quantity);
        public bool ExChangeformInvoice(int InvoiceID, int Quantity);
        public DtoFollowingSaleAdminstrarive GetInvoiceBetweenSaleAdminstrativeByID(int ID);
        public bool ConfirmBySale(DtoFollowingSaleAdminstrativeAddforSale dto);
        public DtoFollowingSaleAdminstrativeAddforadminstrativeReturned AddInvoiceByAdminstative(DtoFollowingSaleAdminstrativeAddforadminstrative dto);
        public bool UpdateInvoiceBySale(DtoFollowingSaleAdminstrativeupdateforSale dto);
        public bool UpdateInvoiceByAdminstrative(DtoFollowingSaleAdminstrativeUpdateforAdminstrative dto);
        public bool DeleteInvoicebyID(int ID, int adminstrativeID);
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeAt(string Date);
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenSaleAdminstrtivesbySaleIDToday(int SaleID);

        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenAdminstrativeSalesbyAdminstrativeID(int AdminstrativeID);
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenSaleAdminstrtivesbyFirstName(string SaleFirstName);
        public List<DTOFollowingSaleAdminstrativeRead> GetInvoiceBetweenSaleAdminstrtivesbySaleID(int SaleID);
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeAt(string Date, int ProductId);
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeToday();
        public DTOFollowingSaleAdminstrativeRead GetInvoiceBetweenSaleAdminstrativeDetailsByID(int ID);
        public List<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrative();


    }
}
