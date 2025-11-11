using Juhyna_BLL.Order.InterFace;
using Juhyna_DAL.Orders.DTO;
using Juhyna_DAL.Orders.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Order.Order
{
    public class ClsOrder:IOrderBLL
    {
        private readonly IOrderDAL _orderDAL;
        public ClsOrder(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }
        public List<DtoOrderRead> GetAllOrder()
        {
            return _orderDAL.GetAllOrder();
        }
        public List<DtoOrderRead> GetAllOrderCompleted()
        {
            return _orderDAL.GetAllOrderCompleted();
        }
        public List<DtoOrderRead> GetAllOrderCanceled()
        {
            return _orderDAL.GetAllOrderCanceled();

        }

        public DtoOrderRead GetOrderById(int ID)
        {
            return _orderDAL.GetOrderById(ID);  
        }

        public DtoOrderRead AddOrder(DtoOrderAdd OrderAdd)
        {
          return  _orderDAL.AddOrder(OrderAdd);   
        }

        public DToOrderUpdate UpdateOrder(DToOrderUpdate order)
        {
            return _orderDAL.UpdateOrder(order);    
        }

        public bool DeleteOrder(int ID, int InvoiceID)
        {
            return _orderDAL.DeleteOrder(ID, InvoiceID);    
        }
    }
}
