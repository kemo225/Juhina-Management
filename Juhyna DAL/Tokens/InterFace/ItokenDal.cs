using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Tokens.InterFace
{
    public interface ItokenDal
    {
        public List<Models.TokenAdmin> GetAllTokenAdmins();
        public List<Models.TokenAdminstrative> GetAllTokenAdminstratives();
        public List<Models.TokenSale> GetAllTokenSales();
    }
}
