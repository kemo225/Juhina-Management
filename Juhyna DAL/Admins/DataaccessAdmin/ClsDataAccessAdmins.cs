using AutoMapper;
using Azure.Core;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Admins.InterFace;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Models;
using Juhyna_DAL.Services.DTO_Token;
using Juhyna_DAL.Services.HashShA256.NewFolder;
using Juhyna_DAL.Services.InterFace_Token;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Admins.DataaccessAdmin
{
    public class ClsDataAccessAdmins : IAdminDAL
    {
        private readonly IMapper _mapper;
        private readonly JuhinaDBContext _DBJuhina;
        private readonly IToken _Token;
        private readonly IhashingDal _hashingDal;

        public ClsDataAccessAdmins(IhashingDal ihashingDal,IMapper mapper, JuhinaDBContext DBJuhina,IToken tokenAdmin)
        {
            _mapper = mapper;
            _DBJuhina = DBJuhina;
            _Token = tokenAdmin;
            _hashingDal = ihashingDal;
        }
        public List<DtoAdminRead> GetAdmins()
        {
            List<DtoAdminRead> readmins = new List<DtoAdminRead>();

            var admins = _DBJuhina.Admins.Include(ad => ad.Person).AsNoTracking().ToList();
            foreach (var admin in admins)
            {
                readmins.Add(_mapper.Map<DtoAdminRead>(admin));
            }
            return readmins;

        }
        public DtoAdminRead GetAdminReadBYID(int ID)
        {
            var admin = _DBJuhina.Admins.Include(ad => ad.Person).FirstOrDefault(ad => ad.ID == ID);
            if (admin != null)
                return _mapper.Map<DtoAdminRead>(admin);
            return null;
        }

        public DtoAdminUpdate AddAdmin(DtoAdminAdd admin)
        {
            var adminadd = new Admin()
            {
                UserName = admin.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(admin.Password),
                Person =
                new Person()
                {
                    FirstName = admin.FirstName,
                    SecondName = admin.SecondName,
                    ThirdName = admin.ThirdName,
                    LastName = admin.LastName,
                    Phone = admin.Phone,
                    Email = admin.Email,
                    Salary = admin.Salary,
                    Gender = (admin.Gender.ToLower() == "male") ? 1 : 0,

                }
            };
            _DBJuhina.Add(adminadd);
            _DBJuhina.SaveChanges();
            var adminRe = _mapper.Map<DtoAdminUpdate>(adminadd);
            if (adminRe.ID > 0)
                return adminRe;
            return null;
        }
        public bool DeleteAdmin(int ID)
        {
            var admin = _DBJuhina.Admins.Include(a => a.Person).Where(a => a.ID == ID).FirstOrDefault();
            if (admin != null)
            {
                _DBJuhina.Remove(admin);
                _DBJuhina.Remove(admin.Person);
                _DBJuhina.SaveChanges();
                return true;
            }
            return false;
        }
        public bool IsAdminExist(int ID)
        {
            var admin = _DBJuhina.Admins.Find(ID);
            if (admin != null)
            {
                return true;
            }
            return false;
        }
        public DtoAdminUpdate UpdateAdmin(DtoAdminUpdate Admin)
        {
            var ad = _DBJuhina.Admins.Find(Admin.ID);
            if (ad != null)
            {
                ad.Person.FirstName = Admin.FirstName;
                ad.Person.LastName = Admin.LastName;
                ad.Person.ThirdName = Admin.ThirdName;
                ad.Person.Email = Admin.Email;
                ad.UserName = Admin.UsertName;
                ad.Person.Phone = Admin.Phone;
                ad.Person.SecondName = Admin.SecondName;
                ad.Person.Salary = Admin.Salary;
                if (Admin.Gender.ToLower() == "male")
                    ad.Person.Gender = 1;
                else
                    ad.Person.Gender = 2;
                _DBJuhina.SaveChanges();
                var adminre = _mapper.Map<DtoAdminUpdate>(ad);
                return adminre;
            }
            return null;
        }

        public DtoToken Login(DTOPublic.DtoLogin Login)
        {
            var Admin = _DBJuhina.Admins.Where(a => a.UserName == Login.UserName).FirstOrDefault();
            if (Admin == null)
                return null;
            if (!BCrypt.Net.BCrypt.Verify(Login.Password, Admin.Password))
                return null;

            string accessToken= _Token.GenerateAccessTokenForAdmin(Login);
            string refreshToken= _Token.GenerateRefreshTokenForAdmin(Login);
            string saltAccessToken= _hashingDal.GenerateSaltString(16);
            string saltRefreshToken= _hashingDal.GenerateSaltString(16);
            var TokenDB= new TokenAdmin() {
                AccessExpireAt=DateTime.Now.AddMinutes(45),
                RefreshExpireAt=DateTime.Now.AddDays(7),
                IsLogin=true,
                AccessToken = _hashingDal.GenerateHashedPassword(accessToken,saltAccessToken),
                RefreshToken = _hashingDal.GenerateHashedPassword(refreshToken, saltRefreshToken),
                AdminID=Admin.ID
                ,SaltAccessToken=saltAccessToken,
                SaltRefreshToken= saltRefreshToken

            };
            _DBJuhina.TokenAdmins.Add(TokenDB);
            _DBJuhina.SaveChanges();
            return new DtoToken() { RefreshToken=refreshToken,AccessToken=accessToken};
        }
        
       public bool Logout(string RefreshToken,int AdminID)
        {
            var tokenDB = _DBJuhina.TokenAdmins.Where(t => t.AdminID == AdminID && t.IsLogin==true).ToList();
            foreach (var token in tokenDB)
            {
                if (_hashingDal.IsPasswordCorrect(RefreshToken, token.RefreshToken,token.SaltRefreshToken))
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
            var admin = _DBJuhina.Admins.Find(Dto.ID);
            if (admin == null)
                return false;
            if (!BCrypt.Net.BCrypt.Verify(Dto.OldPassword, admin.Password))
                return false;
            admin.Password = BCrypt.Net.BCrypt.HashPassword(Dto.NewPassword);
            _DBJuhina.SaveChanges();
            return true;
        }


    }
}


