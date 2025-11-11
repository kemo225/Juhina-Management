using Juhyna_BLL.Admins.InterFace;
using Juhyna_DAL.Admins.DataaccessAdmin;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Admins.InterFace;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Services.DTO_Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Admins.ClsAdmin
{
    public class ClsAdmin: IAdminBLL
    {
       private readonly IAdminDAL _admin;
        public ClsAdmin(IAdminDAL admin)
        {
            _admin = admin;
        }
        public List<DtoAdminRead>GetAdmins()
        {
           return _admin.GetAdmins();
        }
        public DtoAdminRead GetAdminReadBYID(int id)
        {
            var admin= _admin.GetAdminReadBYID(id);
            if (admin != null)
                return admin;
            return null;
        }
        public DtoAdminUpdate AddAdmin(DtoAdminAdd ad)
        {
            var admin = _admin.AddAdmin(ad);
            if (admin != null)
                return admin;
            return null;
        }
        public DtoAdminUpdate UpdateAdmin(DtoAdminUpdate ad)
        {
            var admin = _admin.UpdateAdmin(ad);
            if (admin != null)
                return admin;
            return null;
        }
        public bool DeleteAdmin(int id)
        {
            return _admin.DeleteAdmin(id);  
        }
        public bool IsAdminExist(int ID)
        {
            return _admin.IsAdminExist(ID);
        }
        public DtoToken Login(DtoLogin Login)
        {
            return _admin.Login(Login);
        }
        public bool Logout(string RefreshToken, int AdminID)
        {
            return _admin.Logout(RefreshToken, AdminID);
        }
        public bool ChangePassword(DToChangePassword dto)
        {
            return _admin.ChangePassword(dto);
        }
    }
}
