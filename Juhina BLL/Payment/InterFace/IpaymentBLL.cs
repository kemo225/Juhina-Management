using Juhyna_DAL.Catogrey.DTO;
using Juhyna_DAL.PaymentMethod.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Payment.InterFace
{
    public interface IpaymentBLL
    {
        public List<DtoPaymentRead> GetAllPaymentMethods();
        public DtoPaymentRead GetPaymentMethodbyID(int id);
    }
}
