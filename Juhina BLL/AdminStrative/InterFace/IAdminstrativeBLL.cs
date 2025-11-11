using Juhyna_DAL.Adminstrative.DTO;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Services.DTO_Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.AdminStrative.InterFace
{
    public interface IAdminstrativeBLL
    {
        public DTOAdminstrativeRead GetallAdminStrativesinInventoryID(int InventoryID);
        public DtoToken Login(Juhyna_DAL.DTOPublic.DtoLogin Login);
        public bool ChangePassword(DToChangePassword dto);
        public List<DTOAdminstrativeRead> GetAdminStratives();
        public DTOAdminstrativeRead GetAdminStrativeReadBYID(int id);
        public DtoAdminStrativeUpdate AddAdminStrative(DTOAdminStrativeAdd admin);
        public DtoAdminStrativeUpdate UpdateAdminStrative(DtoAdminStrativeUpdate admin);
        public bool DeleteAdminStrative(int ID);
        public bool IsAdminStrativeExist(int id);
        public bool Logout(string RefreshToken, int AdminstrativeID);

    }
}
