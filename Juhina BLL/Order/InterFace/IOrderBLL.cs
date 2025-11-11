using Juhyna_DAL.Orders.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Order.InterFace
{
    public interface IOrderBLL
    {
        public bool DeleteOrder(int ID, int InvoiceID);
        public List<DtoOrderRead> GetAllOrder();
        public List<DtoOrderRead> GetAllOrderCanceled();
        public List<DtoOrderRead> GetAllOrderCompleted();
        public DtoOrderRead GetOrderById(int ID);
        public DtoOrderRead AddOrder(DtoOrderAdd OrderAdd);
        public DToOrderUpdate UpdateOrder(DToOrderUpdate order);
    }
}
