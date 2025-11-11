using Juhyna_DAL.Catogrey.DTO;
using Juhyna_DAL.PaymentMethod.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.PaymentMethod.InterFace
{
    public interface IPaymentDAL
    {
        public List<DtoPaymentRead> GetAllPaymentMethods();
        public DtoPaymentRead GetPaymentMethodbyID(int id);
    }
}
