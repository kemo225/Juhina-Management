using Juhyna_DAL.ManageProductInInventoryByAdminstrative.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.ManageProductInventory.InterFace
{
    public interface IManageProductInevntoryBLL
    {
        public bool FillProductinInventory(DtoFillProductInventory dto);
        //public bool ExchangeProductinInventory(DtoExcahngeProductInventory dto);
        public List<DtoManageProductWithAdminstrativeRead> GetAllManageProductsByAdminstrative();
    }
}
