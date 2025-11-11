using Juhyna_DAL.Catogrey.DTO;
using Juhyna_DAL.PaymentMethod.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Catogrey.InterFace
{
    public interface ICatogreyDAL
    {
        public List<DtoCatogreyRead> GetAllCatogrey();
        public DtoCatogreyRead GetCatogreybyID(int id);
    }
}
