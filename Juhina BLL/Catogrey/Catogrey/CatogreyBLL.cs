using Juhyna_BLL.Catogrey.ICatogreyBLL;
using Juhyna_DAL.Catogrey.DTO;
using Juhyna_DAL.Catogrey.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Catogrey.Catogrey
{
    public class CatogreyBLL: IcatogreyBLL
    {
        private readonly ICatogreyDAL _catogreyDAL;
        public CatogreyBLL(ICatogreyDAL catogreyDAL)
        {
            _catogreyDAL = catogreyDAL;
        }
        public List<DtoCatogreyRead> GetAllCatogrey()
        {
            return _catogreyDAL.GetAllCatogrey();
        }
        public DtoCatogreyRead GetCatogreybyID(int id)
            {
            return _catogreyDAL.GetCatogreybyID(id);
        }
    }
}
