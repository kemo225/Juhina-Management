using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Models;
using Juhyna_DAL.Services.InterFace_Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Services.JWT
{
    public class JwtToken:IToken
    {
        private readonly JuhinaDBContext _JughinaDB;
        public JwtToken(JuhinaDBContext JughinaDB)
        {
            _JughinaDB = JughinaDB;
        }
        public string GenerateAccessTokenForAdmin(DtoLogin Login)
        {
            var Admin=_JughinaDB.Admins.Include(A=>A.Person).Where(A=>A.UserName == Login.UserName).FirstOrDefault();

            //Create Claims
            var Claimss = new List<Claim>();
            Claimss.Add(new Claim(ClaimTypes.Name, Admin.Person.FirstName + " " + Admin.Person.LastName));
            Claimss.Add(new Claim("ID", Admin.ID.ToString()));
            Claimss.Add(new Claim("Phone", Admin.Person.Phone));
            Claimss.Add(new Claim(ClaimTypes.Role, "Admin"));


            // Create Secret Key
            string Secretkey = "osidcfgw#$%#$@_()_!+_PWOSLX>W)EF(GIKFLD";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));

            //  Create Signature
            var signcredials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // Header Alg Type
            // Payload CLaims Expire 
            // Signature Hash(Secret key+Header+Payload)

            var Token = new JwtSecurityToken(
             claims: Claimss,
             expires: DateTime.Now.AddMinutes(45),
             signingCredentials: signcredials
             );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
        public string GenerateForAccessTokenAdminstrative(DtoLogin Login)
        {
            var Adminstrative = _JughinaDB.Adminstratives.Include(A => A.Person).Where(A => A.UserName == Login.UserName).FirstOrDefault();

            //Create Claims
            var Claimss = new List<Claim>();
            Claimss.Add(new Claim(ClaimTypes.Name, Adminstrative.Person.FirstName + " " + Adminstrative.Person.LastName));
            Claimss.Add(new Claim("ID", Adminstrative.ID.ToString()));
            Claimss.Add(new Claim("Phone", Adminstrative.Person.Phone));
            Claimss.Add(new Claim(ClaimTypes.Role, "Adminstrative"));

            // Create Secret Key
            string Secretkey = "osidcfgw#$%#$@_()_!+_PWOSLX>W)EF(GIKFLD";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));

            //  Create Signature
            var signcredials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // Header Alg Type
            // Payload CLaims Expire 
            // Signature Hash(Secret key+Header+Payload)

            var Token = new JwtSecurityToken(
             claims: Claimss,
             expires: DateTime.Now.AddMinutes(45),
             signingCredentials: signcredials
             );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
        public string GenerateAccessTokenForSale(DtoLogin Login)
        {
            var Sale = _JughinaDB.Sales.Include(A => A.Person).Where(A => A.UserName == Login.UserName).FirstOrDefault();

            //Create Claims
            var Claimss = new List<Claim>();
            Claimss.Add(new Claim(ClaimTypes.Name, Sale.Person.FirstName + " " + Sale.Person.LastName));
            Claimss.Add(new Claim("ID", Sale.ID.ToString()));
            Claimss.Add(new Claim("Phone", Sale.Person.Phone));
            Claimss.Add(new Claim(ClaimTypes.Role, "Sale"));

            // Create Secret Key
            string Secretkey = "osidcfgw#$%#$@_()_!+_PWOSLX>W)EF(GIKFLD";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));

            //  Create Signature
            var signcredials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // Header Alg Type
            // Payload CLaims Expire 
            // Signature Hash(Secret key+Header+Payload)

            var Token = new JwtSecurityToken(
             claims: Claimss,
             expires: DateTime.Now.AddMinutes(45),
             signingCredentials: signcredials
             );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
        public string GenerateRefreshTokenForAdmin(DtoLogin Login)
        {
            var Admin = _JughinaDB.Admins.Include(A => A.Person).Where(A => A.UserName == Login.UserName).FirstOrDefault();

            //Create Claims
            var Claimss = new List<Claim>();
            Claimss.Add(new Claim(ClaimTypes.Name, Admin.Person.FirstName + " " + Admin.Person.LastName));
            Claimss.Add(new Claim("ID", Admin.ID.ToString()));
            Claimss.Add(new Claim("Phone", Admin.Person.Phone));
            Claimss.Add(new Claim(ClaimTypes.Role, "Admin"));


            // Create Secret Key
            string Secretkey = "osidcfgw#$%#$@_()_!+_PWOSLX>W)EF(GIKFLD";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));

            //  Create Signature
            var signcredials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // Header Alg Type
            // Payload CLaims Expire 
            // Signature Hash(Secret key+Header+Payload)

            var Token = new JwtSecurityToken(
             claims: Claimss,
             expires: DateTime.Now.AddDays(7),
             signingCredentials: signcredials
             );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
        public string GenerateForRefreshTokenAdminstrative(DtoLogin Login)
        {
            var Adminstrative = _JughinaDB.Adminstratives.Include(A => A.Person).Where(A => A.UserName == Login.UserName).FirstOrDefault();

            //Create Claims
            var Claimss = new List<Claim>();
            Claimss.Add(new Claim(ClaimTypes.Name, Adminstrative.Person.FirstName + " " + Adminstrative.Person.LastName));
            Claimss.Add(new Claim("ID", Adminstrative.ID.ToString()));
            Claimss.Add(new Claim("Phone", Adminstrative.Person.Phone));
            Claimss.Add(new Claim(ClaimTypes.Role, "Adminstrative"));

            // Create Secret Key
            string Secretkey = "osidcfgw#$%#$@_()_!+_PWOSLX>W)EF(GIKFLD";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));

            //  Create Signature
            var signcredials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // Header Alg Type
            // Payload CLaims Expire 
            // Signature Hash(Secret key+Header+Payload)

            var Token = new JwtSecurityToken(
             claims: Claimss,
             expires: DateTime.Now.AddDays(7),
             signingCredentials: signcredials
             );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
        public string GenerateRefreshTokenForSale(DtoLogin Login)
        {
            var Sale = _JughinaDB.Sales.Include(A => A.Person).Where(A => A.UserName == Login.UserName).FirstOrDefault();

            //Create Claims
            var Claimss = new List<Claim>();
            Claimss.Add(new Claim(ClaimTypes.Name, Sale.Person.FirstName + " " + Sale.Person.LastName));
            Claimss.Add(new Claim("ID", Sale.ID.ToString()));
            Claimss.Add(new Claim("Phone", Sale.Person.Phone));
            Claimss.Add(new Claim(ClaimTypes.Role, "Sale"));

            // Create Secret Key
            string Secretkey = "osidcfgw#$%#$@_()_!+_PWOSLX>W)EF(GIKFLD";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));

            //  Create Signature
            var signcredials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // Header Alg Type
            // Payload CLaims Expire 
            // Signature Hash(Secret key+Header+Payload)

            var Token = new JwtSecurityToken(
             claims: Claimss,
             expires: DateTime.Now.AddDays(7),
             signingCredentials: signcredials
             );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
