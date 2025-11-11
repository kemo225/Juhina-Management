using Juhyna_BLL.Payment.InterFace;
using Juhyna_DAL.Catogrey.DTO;
using Juhyna_DAL.Catogrey.InterFace;
using Juhyna_DAL.PaymentMethod.DTO;
using Juhyna_DAL.PaymentMethod.InterFace;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Payment.Paymentmethod
{
    public class PaymentMethod:IpaymentBLL
    {
        private readonly IPaymentDAL _paymentDAL;
        public PaymentMethod(IPaymentDAL paymentDAL)
        {
            _paymentDAL = paymentDAL;
        }
        public List<DtoPaymentRead> GetAllPaymentMethods()
        {
              return _paymentDAL.GetAllPaymentMethods();
        }
        public DtoPaymentRead GetPaymentMethodbyID(int id)
        {
            return _paymentDAL.GetPaymentMethodbyID(id);
        }
    }
}
