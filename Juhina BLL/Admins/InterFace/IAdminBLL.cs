using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Services.DTO_Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Admins.InterFace
{
    public interface IAdminBLL
    {
        public List<DtoAdminRead> GetAdmins();
        public DtoAdminRead GetAdminReadBYID(int id);
        public DtoAdminUpdate AddAdmin(DtoAdminAdd admin);
        public DtoAdminUpdate UpdateAdmin(DtoAdminUpdate admin);
        public bool DeleteAdmin(int ID);
        public bool IsAdminExist(int id);
        public DtoToken Login(Juhyna_DAL.DTOPublic.DtoLogin Login);
        public bool ChangePassword(DToChangePassword dto);
        public bool Logout(string RefreshToken, int AdminID);

    }
}
