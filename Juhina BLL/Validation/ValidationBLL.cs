using Juhyna_DAL.Models;
using Juhyna_DAL.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Validation
{
    public class ValidationBLL
    {
        private readonly ValidationDAL _ValidtionDal;
        public ValidationBLL(ValidationDAL validtion)
        {
            _ValidtionDal = validtion;
        }
        public bool IsCandoReturn(int QuantityReturn, int Quantityinvoice)
        {
            return _ValidtionDal.IsCandoReturn(QuantityReturn, Quantityinvoice);
        }
        public bool IsCanDoOrder(int Rest, int QuantityBought)
        {
            return _ValidtionDal.IsCanDoOrder(Rest, QuantityBought);    
        }
        public bool IsCanUpdateOrder(string StatusOrder)
        {
            return _ValidtionDal.IsCanUpdateOrder(StatusOrder);
        }
    }
}
