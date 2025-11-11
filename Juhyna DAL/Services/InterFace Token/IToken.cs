using Juhyna_DAL.DTOPublic;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Services.InterFace_Token
{
    public interface IToken
    {
        public string GenerateAccessTokenForAdmin(DtoLogin Login);
        public string GenerateForAccessTokenAdminstrative(DtoLogin Login);
        public string GenerateAccessTokenForSale(DtoLogin Login);
        public string GenerateRefreshTokenForAdmin(DtoLogin Login);
        public string GenerateForRefreshTokenAdminstrative(DtoLogin Login);
        public string GenerateRefreshTokenForSale(DtoLogin Login);
    }
}
