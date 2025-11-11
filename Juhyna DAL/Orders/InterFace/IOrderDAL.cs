using Juhyna_DAL.Models;
using Juhyna_DAL.Orders.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Orders.InterFace
{
    public interface IOrderDAL
    {

        public List<DtoOrderRead> GetAllOrder();
        public List<DtoOrderRead> GetAllOrderCanceled();
        public List<DtoOrderRead> GetAllOrderCompleted();
        public bool DeleteOrder(int ID, int InvoiceID);
        public DtoOrderRead GetOrderById(int ID);

        public DtoOrderRead AddOrder(DtoOrderAdd OrderAdd);

        public DToOrderUpdate UpdateOrder(DToOrderUpdate order);
    }
}
