using Juhyna_BLL.Sales.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Sales.DtoSales;
using Juhyna_DAL.Sales.InterFace;
using Juhyna_DAL.Services.DTO_Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Sales.clsSales
{
    public class ClsSales:ISalesBLL
    {
        private readonly ISales _sale;
        public ClsSales(ISales sale)
        {
            _sale = sale;
        }
        public List<DTOsalesRead> GetSales()
        {
            return _sale.GetSales();
        }
        public DTOsalesRead GetSalesById(int id)
        {
            var sale = _sale.GetSalesById(id);
            if (sale != null)
                return sale;
            return null;
        }
        public DTOSalesUpdate AddSale(DTOSalesAdd sale)
        {
            var saleadd = _sale.AddSale(sale);
            if (saleadd != null)
                return saleadd;
            return null;
        }
        public DTOSalesUpdate UpdateSale(DTOSalesUpdate sale)
        {
            var saleupdaet = _sale.UpdateSale(sale);
            if (saleupdaet != null)
                return saleupdaet;
            return null;
        }
        public bool DeleteSale(int id)
        {
            return _sale.DeleteSale(id);
        }
        public bool IsSaleExist(int ID)
        {
            return _sale.IsSaleExist(ID);
        }
        public DtoToken Login(DtoLogin Login)
        {
            return _sale.Login(Login);
        }
        public bool Logout(string RefreshToken, int SaleID)
        {
            return _sale.Logout(RefreshToken, SaleID);  
        }

        public bool ChangePassword(DToChangePassword dto)
        {
            return _sale.ChangePassword(dto);
        }
    }

}
