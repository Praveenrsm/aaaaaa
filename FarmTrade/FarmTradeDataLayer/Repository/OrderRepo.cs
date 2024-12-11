using FarmTradeEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeDataLayer.Repository
{
    public class OrderRepo:IOrderRepo
    {
        private readonly FarmContext _context;

        public OrderRepo(FarmContext context)
        {
            _context = context;
        }

        public Order AddOrder(Order order)
        {
            _context.orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order GetOrderById(int orderId)
        {
            return _context.orders
                .Include(c => c.user).Include(o=> o.address)
                .FirstOrDefault(o => o.Id == orderId);
        }
        public IEnumerable<Order> GetOrders()
        {
            #region GET All Orders
            return _context.orders.ToList();
            #endregion
        }
        public void UpdateOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
            //_context.orders.Update(order);
            //_context.SaveChanges();
        }
    }
}
