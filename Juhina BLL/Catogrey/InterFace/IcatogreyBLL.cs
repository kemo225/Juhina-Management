using Juhyna_DAL.Catogrey.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Catogrey.ICatogreyBLL
{
    public interface IcatogreyBLL
    {
        public List<DtoCatogreyRead> GetAllCatogrey();
        public DtoCatogreyRead GetCatogreybyID(int id);
    }
}
