using FarmTradeDataLayer;
using FarmTradeDataLayer.Repository;
using FarmTradeEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FarmBusiness.Services
{
    public class OrderService
    {
        IOrderRepo _orderRepo;
        FarmContext _context;
        public OrderService(IOrderRepo orderRepo, FarmContext context)
        {
            _orderRepo = orderRepo;
            _context = context;
        }
        public void PlaceOrder(Guid userId,int addressId1)
        {
            // Create new order
            var order = new Order
            {
                userId = userId,
                orderDate = DateTime.Now,
                addressId = addressId1
            };

            _orderRepo.AddOrder(order);

            // Move cart items to order items
            var cartItems = _context.cart.Where(c => c.userId == userId).ToList();
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    productId = item.productId,
                    Quantity = item.Quantity,
                    UnitPrice = item.totalPrice
                };
                _context.orderItem.Add(orderItem);
            }

            // Clear the cart after placing the order
            _context.cart.RemoveRange(cartItems);
            _context.SaveChanges();

            // Notify suppliers about the new order
            NotifySuppliers(order.Id);
        }

        private void NotifySuppliers(int orderId)
        {
            var orderItems = _context.orderItem.Where(oi => oi.OrderId == orderId).ToList();
            var suppliers = _context.product.Include(p => p.User)
                .Where(s => orderItems.Select(oi => oi.productId).Contains(s.ProductId))
                .ToList();

            foreach (var supplier in suppliers)
            {
                // Customize your message
                string subject = $"New Order #{orderId} Notification";
                string body = BuildOrderNotificationEmailBody(supplier, orderId, orderItems);

                // Send email
                SendEmail(supplier.User.Email, subject, body);
            }
        }
        private string BuildOrderNotificationEmailBody(Product supplier, int orderId, List<OrderItem> orderItems)
        {
            var sb = new StringBuilder();
            if (supplier.User == null || string.IsNullOrEmpty(supplier.User.UserName))
            {
                sb.AppendLine("Hello Supplier,");
            }
            else
            {
                sb.AppendLine($"Hello {supplier.User.UserName},");
            }

            sb.AppendLine();
            sb.AppendLine($"You have received a new order (Order ID: {orderId}). Here are the details:");
            sb.AppendLine();

            foreach (var item in orderItems.Where(oi => oi.productId == supplier.ProductId))
            {
                sb.AppendLine($"- Product ID: {item.productId}, Quantity: {item.Quantity}, Price: {item.UnitPrice:C}");
            }

            sb.AppendLine();
            sb.AppendLine("Please prepare for fulfillment.");
            sb.AppendLine("Thank you!");

            return sb.ToString();
        }

        private void SendEmail(string recipientEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.your-email-provider.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("praveenrsm1234@gmail.com", "Praveen0077$$"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("Praveenrsm1234@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add("praveen.rajendran@valtech.com");

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email to {recipientEmail}. Error: {ex.Message}");
                // Optionally log the exception or handle it as needed
            }
        }

    }
}
