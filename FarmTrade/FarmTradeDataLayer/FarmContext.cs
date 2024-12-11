using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using FarmTradeEntity;

namespace FarmTradeDataLayer
{
    public class FarmContext:DbContext
    {
        public FarmContext()
        {
        }

        public FarmContext(DbContextOptions<FarmContext> options) : base(options)
        {

        }

        // Entity class database:

        public DbSet<User> users { get; set; }
        public DbSet<ReviewsAndRatings> ratingsreview { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<Address> address { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItem { get; set; }
        // SQL Connection:
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Server=IN-6YYZFY3;Database=Farmingprod;User Id=sa;Password=Praveen0077$$$$;TrustServerCertificate=True;");
        }
    }
}
