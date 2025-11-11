using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Sales.DtoSales;
using Juhyna_DAL.Services.DTO_Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Sales.InterFace
{
    public interface ISales
    {
        public List<DTOsalesRead> GetSales();
        public DTOsalesRead GetSalesById(int id);
        public DTOSalesUpdate UpdateSale(DTOSalesUpdate sale);
        public DTOSalesUpdate AddSale(DTOSalesAdd sale);
        public bool DeleteSale(int id);
        public bool IsSaleExist(int id);
        public DtoToken Login(DTOPublic.DtoLogin Login);
        public bool Logout(string RefreshToken, int SaleID);

        public bool ChangePassword(DToChangePassword Dto);

    }
}
