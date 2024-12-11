using FarmTradeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeDataLayer.Repository
{
    public interface IproductRepository
    {
        void UpdateProduct(Product product, List<byte[]> imageList);
        string AddProduct(Product product, List<byte[]> imageList);
        Product GetProductWithReviews(int productId);
        IEnumerable<Product> GetAllProducts();
        void DeleteProduct(int productId);
    }
}