using FarmTradeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeDataLayer.Repository
{
    public interface IOrderRepo
    {
        void UpdateOrder(Order order);
        Order AddOrder(Order order);
        Order GetOrderById(int orderId);
        IEnumerable<Order> GetOrders();
    }
}
