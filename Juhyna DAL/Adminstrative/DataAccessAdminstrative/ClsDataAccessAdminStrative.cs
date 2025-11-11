using AutoMapper;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Admins.InterFace;
using Juhyna_DAL.Adminstrative.DTO;
using Juhyna_DAL.Adminstrative.InterFace;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.Models;
using Juhyna_DAL.Services.DTO_Token;
using Juhyna_DAL.Services.HashShA256.NewFolder;
using Juhyna_DAL.Services.InterFace_Token;


//using Juhyna_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Adminstrative.DataAccessAdminstrative
{
    public class ClsDataAccessAdminStrative : IAdminstrative
    {
        private readonly IMapper _mapper;
        private readonly JuhinaDBContext _DBJuhina;
        private readonly IToken _Token;
        private readonly IhashingDal _hashingDal;
        public ClsDataAccessAdminStrative(IhashingDal ihashingDal,IMapper mapper, JuhinaDBContext DBJuhina, IToken tokenAdminstrative)
        {
            _mapper = mapper;
            _DBJuhina = DBJuhina;
            _Token = tokenAdminstrative;
            _hashingDal = ihashingDal;
        }
        public List<DTOAdminstrativeRead> GetAdminStratives()
        {
            List<DTOAdminstrativeRead> Adminstrativereturn = new List<DTOAdminstrativeRead>();

            var Adminstratives = _DBJuhina.Adminstratives.Include(ad => ad.Person).Include(a => a.Inventory).AsNoTracking().ToList();
            foreach (var admin in Adminstratives)
            {
                Adminstrativereturn.Add(_mapper.Map<DTOAdminstrativeRead>(admin));
            }
            return Adminstrativereturn;

        }
        public DTOAdminstrativeRead GetAdminStrativeReadBYID(int ID)
        {
            var AdminStrative = _DBJuhina.Adminstratives.Include(ad => ad.Person).Include(a => a.Inventory).FirstOrDefault(ad => ad.ID == ID);
            if (AdminStrative != null)
                return _mapper.Map<DTOAdminstrativeRead>(AdminStrative);
            return null;
        }
        public DTOAdminstrativeRead GetallAdminStrativesinInventoryID(int InventoryID)
        {
            var AdminStrative = _DBJuhina.Adminstratives.Include(ad => ad.Person).Include(a => a.Inventory).FirstOrDefault(ad => ad.InventoryID == InventoryID);
            if (AdminStrative != null)
                return _mapper.Map<DTOAdminstrativeRead>(AdminStrative);
            return null;
        }
        public DtoAdminStrativeUpdate AddAdminStrative(DTOAdminStrativeAdd adminstrative)
        {
            var adminstrativeadd = new Models.Adminstrative()
            {
                UserName = adminstrative.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(adminstrative.Password),
                InventoryID = adminstrative.InventoryID,
                Person =
                new Person()
                {
                    FirstName = adminstrative.FirstName,
                    SecondName = adminstrative.SecondName,
                    ThirdName = adminstrative.ThirdName,
                    LastName = adminstrative.LastName,
                    Phone = adminstrative.Phone,
                    Email = adminstrative.Email,
                    Salary = adminstrative.Salary,
                    Gender = (adminstrative.Gender.ToLower() == "male") ? 1 : 0,


                }
            };
            _DBJuhina.Add(adminstrativeadd);
            _DBJuhina.SaveChanges();
            var adminstrativere = _mapper.Map<DtoAdminStrativeUpdate>(adminstrativeadd);
            if (adminstrativere.ID > 0)
                return adminstrativere;
            return null;
        }
        public bool DeleteAdminStrative(int ID)
        {
            var AdminStrative = _DBJuhina.Adminstratives.Include(a => a.Person).Where(a => a.ID == ID).FirstOrDefault();
            if (AdminStrative != null)
            {
                _DBJuhina.Remove(AdminStrative);
                _DBJuhina.Remove(AdminStrative.Person);
                _DBJuhina.SaveChanges();
                return true;
            }
            return false;
        }
        public bool IsAdminStrativeExist(int ID)
        {
            var AdminStrative = _DBJuhina.Adminstratives.Find(ID);
            if (AdminStrative != null)
            {
                return true;
            }
            return false;
        }
        public DtoAdminStrativeUpdate UpdateAdminStrative(DtoAdminStrativeUpdate Admin)
        {
            var Adminstrative = _DBJuhina.Adminstratives.Find(Admin.ID);
            if (Adminstrative != null)
            {
                Adminstrative.Person.FirstName = Admin.FirstName;
                Adminstrative.Person.LastName = Admin.LastName;
                Adminstrative.Person.ThirdName = Admin.ThirdName;
                Adminstrative.Person.Email = Admin.Email;
                Adminstrative.UserName = Admin.UsertName;
                Adminstrative.Person.Phone = Admin.Phone;
                Adminstrative.Person.SecondName = Admin.SecondName;
                Adminstrative.Person.Salary = Admin.Salary;
                Adminstrative.InventoryID = Admin.InvrnotyID;
                if (Admin.Gender.ToLower() == "male")
                    Adminstrative.Person.Gender = 1;
                else
                    Adminstrative.Person.Gender = 2;
                _DBJuhina.SaveChanges();
                var AdminstrativeRE = _mapper.Map<DtoAdminStrativeUpdate>(Adminstrative);
                return AdminstrativeRE;
            }
            return null;
        }
        public DtoToken Login(DTOPublic.DtoLogin Login)
        {
            var Adminstrative = _DBJuhina.Adminstratives.Where(a => a.UserName == Login.UserName).FirstOrDefault();
            if (Adminstrative == null)
                return null;
            if (!BCrypt.Net.BCrypt.Verify(Login.Password, Adminstrative.Password))
                return null;

            string accessToken = _Token.GenerateForAccessTokenAdminstrative(Login);
            string refreshToken = _Token.GenerateForRefreshTokenAdminstrative(Login);
            string saltAccessToken = _hashingDal.GenerateSaltString(16);
            string saltRefreshToken = _hashingDal.GenerateSaltString(16);

            var TokenDB = new TokenAdminstrative()
            {
                AccessExpireAt = DateTime.Now.AddMinutes(45),
                RefreshExpireAt = DateTime.Now.AddDays(7),
                IsLogin = true,
                AccessToken = _hashingDal.GenerateHashedPassword(accessToken,saltAccessToken),
                RefreshToken = _hashingDal.GenerateHashedPassword(refreshToken, saltRefreshToken),
                AdminstrativeID = Adminstrative.ID
                ,
                SaltAccessToken = saltAccessToken,
                SaltRefreshToken = saltRefreshToken

            };
            _DBJuhina.TokenAdminstratives.Add(TokenDB);
            _DBJuhina.SaveChanges();
            return new DtoToken() { RefreshToken = refreshToken, AccessToken = accessToken };

        }
        public bool Logout(string RefreshToken, int AdminstrativeID)
        {
            var tokenDB = _DBJuhina.TokenAdminstratives.Where(t => t.AdminstrativeID == AdminstrativeID && t.IsLogin == true).ToList();
            foreach (var token in tokenDB)
            {
                if (_hashingDal.IsPasswordCorrect(RefreshToken, token.RefreshToken, token.SaltRefreshToken))
                {
                    token.IsLogin = false;
                    _DBJuhina.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool ChangePassword(DToChangePassword Dto)
        {
            var Adminstrative = _DBJuhina.Adminstratives.Find(Dto.ID);
            if (Adminstrative == null)
                return false;
            if (!BCrypt.Net.BCrypt.Verify(Dto.OldPassword, Adminstrative.Password))
                return false;
            Adminstrative.Password = BCrypt.Net.BCrypt.HashPassword(Dto.NewPassword);
            _DBJuhina.SaveChanges();
            return true;
        }
    }
}
