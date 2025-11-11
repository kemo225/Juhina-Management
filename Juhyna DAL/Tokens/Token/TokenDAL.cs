using Juhyna_DAL.Models;
using Juhyna_DAL.Tokens.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Tokens.Token
{
    public class TokenDAL:ItokenDal
    {
        private readonly JuhinaDBContext _context;
        public TokenDAL(JuhinaDBContext context)
        {
            _context = context;
        }
        public List<TokenAdmin> GetAllTokenAdmins()
        {
            return _context.TokenAdmins.ToList();
        }
        public List<TokenAdminstrative> GetAllTokenAdminstratives()
        {
            return _context.TokenAdminstratives.ToList();

        }
        public List<TokenSale> GetAllTokenSales()
        {
            return _context.TokenSales.ToList();
        }

    }
}
