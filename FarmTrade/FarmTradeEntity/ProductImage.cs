using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeEntity
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; } // Primary key for the image entry

        [AllowNull]
        public byte[] ImageData { get; set; } // Byte array to store image data

        [ForeignKey("Product")]
        public int ProductId { get; set; } // Foreign key to the Product

        public Product Product { get; set; }
        //public IFormFile? formFile { get; set; }
    }
}
