using Juhyna_BLL.Token.InterFace;
using Juhyna_DAL.Tokens.InterFace;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Token.Token
{
    public class TokenBLL:ItokenBLL
    {
        private readonly ItokenDal _TokenDAL;
        public TokenBLL(ItokenDal tokenDal)
        {
            _TokenDAL = tokenDal;
        }   
        public List<Juhyna_DAL.Models.TokenAdmin> GetAllTokenAdmins()
        {
            return _TokenDAL.GetAllTokenAdmins();
        }
        public List<Juhyna_DAL.Models.TokenAdminstrative> GetAllTokenAdminstratives()
        {
return _TokenDAL.GetAllTokenAdminstratives();
        }
        public List<Juhyna_DAL.Models.TokenSale> GetAllTokenSales()
        {
            return _TokenDAL.GetAllTokenSales();    
        }
    }
}
