using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.AdminStrative.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Admins.InterFace;
using Juhyna_DAL.Adminstrative.DTO;
using Juhyna_DAL.Adminstrative.InterFace;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Models;
using Juhyna_DAL.Sales.InterFace;
using Juhyna_DAL.Services.DTO_Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.AdminStrative.Adminstrative
{
    public class ClsAdminStrative
   : IAdminstrativeBLL
    {
        private readonly IAdminstrative _adminstrative;
        public ClsAdminStrative(IAdminstrative adminstrative)
        {
            _adminstrative = adminstrative;
        }
        public List<DTOAdminstrativeRead> GetAdminStratives()
        {
            return _adminstrative.GetAdminStratives();
        }
        public bool Logout(string RefreshToken, int AdminstrativeID)
        {
            return _adminstrative.Logout(RefreshToken, AdminstrativeID);
        }
        public DTOAdminstrativeRead GetAdminStrativeReadBYID(int id)
        {
            var AdminStrative = _adminstrative.GetAdminStrativeReadBYID(id);
            if (AdminStrative != null)
                return AdminStrative;
            return null;
        }
        public DtoAdminStrativeUpdate AddAdminStrative(DTOAdminStrativeAdd ad)
        {
            var adminstrative = _adminstrative.AddAdminStrative(ad);
            if (adminstrative != null)
                return adminstrative;
            else
                return null;
        }
        public DtoAdminStrativeUpdate UpdateAdminStrative(DtoAdminStrativeUpdate ad)
        {
            var adminstrative = _adminstrative.UpdateAdminStrative(ad);
            if (adminstrative != null)
                return adminstrative;
            else
                return null;
        }
        public bool DeleteAdminStrative(int id)
        {
            return _adminstrative.DeleteAdminStrative(id);
        }
        public bool IsAdminStrativeExist(int ID)
        {
            return _adminstrative.IsAdminStrativeExist(ID);
        }
        public DtoToken Login(DtoLogin Login)
        {
            return _adminstrative.Login(Login);
        }
        public bool ChangePassword(DToChangePassword dto)
        {
            return _adminstrative.ChangePassword(dto);
        }
        public DTOAdminstrativeRead GetallAdminStrativesinInventoryID(int InventoryID)
        {
            return _adminstrative.GetallAdminStrativesinInventoryID(InventoryID);   
        }
    }
}
