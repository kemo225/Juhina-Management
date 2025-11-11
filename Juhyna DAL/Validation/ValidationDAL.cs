using Juhyna_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Validation
{
    public class ValidationDAL
    {
        public bool IsCanDoOrder(int Rest,int QuantityBought)
        {
            return(Rest >= QuantityBought);
        }
        public bool IsCanUpdateOrder(string StatusOrder)
        {
            return (StatusOrder.ToLower() == "completed")? false : true;
        }
        public bool IsCandoReturn(int QuantityReturn,int Quantityinvoice)
        {
            return (Quantityinvoice>= QuantityReturn);
        }
    }
}
