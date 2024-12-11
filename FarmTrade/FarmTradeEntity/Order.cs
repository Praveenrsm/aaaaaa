using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeEntity
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime orderDate { get; set; } = DateTime.Now;

        public OrderStatus status { get; set; } = OrderStatus.confirmed;
        public string PaymentMethod { get; set; } = "Cash On Delivery";

        [ForeignKey("User")]
        public Guid userId { get; set; }
        public User user { get; set; }
        [ForeignKey("Address")]
        public int addressId { get; set; }
        public Address address { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        //[ForeignKey("Product")]
        //public int productId { get; set; }
        //public Product product { get; set; }
    }
}
