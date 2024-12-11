using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace FarmTradeEntity
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        //[AllowNull]
        //public byte[] productImage { get; set; }

        public decimal productPrice { get; set; }
        public string Description { get; set; }
        public int availableQuantity { get; set; }
        [ForeignKey("User")]
        public Guid userId { get; set; }

        public User User { get; set; }
        public ICollection<ReviewsAndRatings> Reviews { get; set; } = new List<ReviewsAndRatings>();
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>(); // Collection for multiple images
    }
}
