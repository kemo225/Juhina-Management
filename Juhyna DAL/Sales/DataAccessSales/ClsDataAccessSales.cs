using AutoMapper;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.Models;
using Juhyna_DAL.Sales.DtoSales;
using Juhyna_DAL.Sales.InterFace;
using Juhyna_DAL.Services.DTO_Token;
using Juhyna_DAL.Services.HashShA256.NewFolder;
using Juhyna_DAL.Services.InterFace_Token;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Sales.DataAccessSales
{
    public class ClsDataAccessSales : ISales
    {
        private readonly JuhinaDBContext _DBJuhina;
        private readonly IMapper _mapper;
        private readonly IToken _Token;
        private readonly IhashingDal _hashingDal;
        public ClsDataAccessSales(IhashingDal ihashingDal,JuhinaDBContext juhinaDB, IMapper mapper,IToken Token)
        {
            _DBJuhina = juhinaDB;
            this._mapper = mapper;
            this._Token = Token;
            _hashingDal = ihashingDal;
        }
        public List<DTOsalesRead> GetSales()
        {
            List<DTOsalesRead> reSales = new List<DTOsalesRead>();

            var sales = _DBJuhina.Sales.AsNoTracking().Include(s => s.Person).Include(s => s.CreatedByAdmin).ThenInclude(Cre => Cre.Person).ToList();
           
            return _mapper.Map<List<DTOsalesRead>>(sales);

        }
        public DTOsalesRead GetSalesById(int ID)
        {
            var sale = _DBJuhina.Sales.Include(s => s.Person).FirstOrDefault(ad => ad.ID == ID);
            if (sale != null)
                return _mapper.Map<DTOsalesRead>(sale);
            return null;
        }
        public DTOSalesUpdate AddSale(DTOSalesAdd sale)
        {
            var saleadd = new Sale()
            {
                UserName = sale.UsertName,
                Password = sale.password,
                CreatedByAdminID = sale.CreatedBYAdminID
                ,
                Person =
                new Person()
                {
                    FirstName = sale.FirstName,
                    SecondName = sale.SecondName,
                    ThirdName = sale.ThirdName,
                    LastName = sale.LastName,
                    Phone = sale.Phone,
                    Email = sale.Email,
                    Salary = sale.Salary,
                    Gender = (sale.Gender.ToLower() == "male") ? 1 : 0,

                }
            };
            _DBJuhina.Add(saleadd);
            _DBJuhina.SaveChanges();
            var resale = _mapper.Map<DTOSalesUpdate>(saleadd);
            if (resale.ID > 0)
                return resale;
            else
                return null;
        }
        public bool DeleteSale(int ID)
        {
            var sale = _DBJuhina.Sales.Include(a => a.Person).Where(a => a.ID == ID).FirstOrDefault();
            if (sale != null)
            {
                _DBJuhina.Remove(sale);
                _DBJuhina.Remove(sale.Person);
                _DBJuhina.SaveChanges();
                return true;
            }
            return false;
        }
        public bool IsSaleExist(int ID)
        {
            var admin = _DBJuhina.Sales.Find(ID);
            if (admin != null)
                return true;
            else
                return false;
        }
        public DTOSalesUpdate UpdateSale(DTOSalesUpdate sale)
        {
            var saleupdate = _DBJuhina.Sales.Find(sale.ID);
            if (saleupdate != null)
            {
                saleupdate.Person.FirstName = sale.FirstName;
                saleupdate.Person.LastName = sale.LastName;
                saleupdate.Person.ThirdName = sale.ThirdName;
                saleupdate.Person.Email = sale.Email;
                saleupdate.UserName = sale.UsertName;
                saleupdate.Person.Phone = sale.Phone;
                saleupdate.Person.SecondName = sale.SecondName;
                saleupdate.Person.Salary = sale.Salary;
                if (sale.Gender.ToLower() == "male")
                    saleupdate.Person.Gender = 1;
                else
                    saleupdate.Person.Gender = 2;
                _DBJuhina.SaveChanges();
                var salere = _mapper.Map<DTOSalesUpdate>(saleupdate);
                return salere;
            }
            return null;
        }
        public DtoToken Login(DTOPublic.DtoLogin Login)
        {
            var Sale = _DBJuhina.Sales.Where(a => a.UserName == Login.UserName).FirstOrDefault();
            if (Sale == null)
                return null;
            if (!BCrypt.Net.BCrypt.Verify(Login.Password, Sale.Password))
                return null;

            string accessToken = _Token.GenerateAccessTokenForSale(Login);
            string refreshToken = _Token.GenerateRefreshTokenForSale(Login);
            string saltAccessToken = _hashingDal.GenerateSaltString(16);
            string saltRefreshToken = _hashingDal.GenerateSaltString(16);

            var TokenDB = new TokenSale()
            {
                AccessExpireAt = DateTime.Now.AddMinutes(45),
                RefreshExpireAt = DateTime.Now.AddDays(7),
                IsLogin = true,
                AccessToken = _hashingDal.GenerateHashedPassword(accessToken, saltAccessToken),
                RefreshToken = _hashingDal.GenerateHashedPassword(refreshToken, saltRefreshToken),
                SaleID = Sale.ID
                ,
                SaltAccessToken = saltAccessToken,
                SaltRefreshToken = saltRefreshToken
            };
            _DBJuhina.TokenSales.Add(TokenDB);
            _DBJuhina.SaveChanges();
            return new DtoToken() { RefreshToken = refreshToken, AccessToken = accessToken };
        }
        public bool Logout(string RefreshToken, int SaleID)
        {
            var tokenDB = _DBJuhina.TokenSales.Where(t => t.SaleID == SaleID && t.IsLogin == true).ToList();
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
            var Sale = _DBJuhina.Sales.Find(Dto.ID);
            if (Sale == null)
                return false;
            ////if (!BCrypt.Net.BCrypt.Verify(Dto.OldPassword, Sale.Password))
            ////    return false;
            Sale.Password = BCrypt.Net.BCrypt.HashPassword(Dto.NewPassword);
            _DBJuhina.SaveChanges();
            return true;
        }
    }
}
