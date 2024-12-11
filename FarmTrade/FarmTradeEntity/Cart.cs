using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeEntity
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        public Product product { get; set; }
        public int Quantity { get; set; } =1;
        public decimal totalPrice { get; set; }
        [ForeignKey("User")]
        public Guid? userId { get; set; }     // Foreign key to User (nullable for non-logged-in users)
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid sessionId { get; set; } = Guid.NewGuid();
    }
}
