using AutoMapper;
using Juhyna_DAL.Catogrey.DTO;
using Juhyna_DAL.PaymentMethod.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.PaymentMethod.Mapper
{
    public class PaymentProfile:Profile
    {
        public PaymentProfile()
        {
            CreateMap<Models.PaymentMethodBLL, DtoPaymentRead>()
                .ForMember(to => to.ID, from => from.MapFrom(from => from.ID))
                .ForMember(to => to.PaymentMethod, from => from.MapFrom(from => from.PaymentMethod1));
        }
    }
}
