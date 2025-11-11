using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Adminstrative.DTO;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Services.DTO_Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Adminstrative.InterFace
{
    public interface IAdminstrative
    {
        public List<DTOAdminstrativeRead> GetAdminStratives();
        public DTOAdminstrativeRead GetAdminStrativeReadBYID(int id);
        public DtoAdminStrativeUpdate AddAdminStrative(DTOAdminStrativeAdd admin);
        public DtoAdminStrativeUpdate UpdateAdminStrative(DtoAdminStrativeUpdate admin);
        public bool DeleteAdminStrative(int ID);
        public bool IsAdminStrativeExist(int id);
        public DtoToken Login(DTOPublic.DtoLogin Login);
        public bool Logout(string RefreshToken, int AdminstrtiveID);

        public bool ChangePassword(DToChangePassword Dto);
        public DTOAdminstrativeRead GetallAdminStrativesinInventoryID(int InventoryID);
    }
}
