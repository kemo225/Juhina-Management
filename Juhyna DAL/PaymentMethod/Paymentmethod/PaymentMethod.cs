using AutoMapper;
using Juhyna_DAL.Catogrey.DTO;
using Juhyna_DAL.Catogrey.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.PaymentMethod.DTO;
using Juhyna_DAL.PaymentMethod.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.PaymentMethod.Paymentmethod
{
    public class PaymentMethod:IPaymentDAL
    {
        private readonly JuhinaDBContext _context;
        private readonly IMapper _mapper;
        public PaymentMethod(JuhinaDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<DtoPaymentRead> GetAllPaymentMethods()
        {
            var paymentMethods = _context.PaymentMethods.ToList();
            if(paymentMethods==null)
                return null;

            return _mapper.Map<List<DtoPaymentRead>>(paymentMethods);
        }
        public DtoPaymentRead GetPaymentMethodbyID(int id)
        {
            var paymentMethod = _context.PaymentMethods.FirstOrDefault(pm => pm.ID == id);
            if (paymentMethod== null)
                return null;
            return _mapper.Map<DtoPaymentRead>(paymentMethod);
        }
    }
}
