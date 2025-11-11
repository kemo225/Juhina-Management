using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Token.InterFace
{
    public interface ItokenBLL
    {
        public List<Juhyna_DAL.Models.TokenAdmin> GetAllTokenAdmins();
        public List<Juhyna_DAL.Models.TokenAdminstrative> GetAllTokenAdminstratives();
        public List<Juhyna_DAL.Models.TokenSale> GetAllTokenSales();
    }
}

