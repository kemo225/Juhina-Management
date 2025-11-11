using AutoMapper;
using Juhyna_BLL.ManageProductInventory.InterFace;
using Juhyna_DAL.ManageProductInInventoryByAdminstrative.DTO;
using Juhyna_DAL.ManageProductInInventoryByAdminstrative.InterFace;
using Juhyna_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.ManageProductInventory.ManageProductInventory
{
    public class ClsManageProductInventory:IManageProductInevntoryBLL
    {
        private readonly IManagementProductInentoryDAl _IManageProductInventoryDAl;
        public ClsManageProductInventory(IManagementProductInentoryDAl IManageProductInventoryDAl)
        {
            _IManageProductInventoryDAl = IManageProductInventoryDAl;
        }
   
        public bool FillProductinInventory(DtoFillProductInventory dto)
        {
          return _IManageProductInventoryDAl.FillProductinInventory(dto);
        }
        //public bool ExchangeProductinInventory(DtoExcahngeProductInventory dto)
        //{
        //    return _IManageProductInventoryDAl.ExchangeProductinInventory(dto);
        //}
        public List<DtoManageProductWithAdminstrativeRead> GetAllManageProductsByAdminstrative()
        {
            return _IManageProductInventoryDAl.GetAllManageProductsByAdminstrative();
        }
    }
}
