using FarmTradeDataLayer.Repository;
using FarmTradeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmBusiness.Services
{
    public class ProductService
    {
        IproductRepository _productRepository;
        public ProductService(IproductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // CRUD Service Operations for PRODUCT:
        public void UpdateProduct(Product product, List<byte[]> imageList)
        {
            _productRepository.UpdateProduct(product, imageList);
        }
        public string AddProduct(Product product, List<byte[]> imageList)
        {
            return _productRepository.AddProduct(product, imageList);
        }
        public Product GetProductWithReviews(int productId)
        {
            return _productRepository.GetProductWithReviews(productId);
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
        public void DeleteProduct(int productId)
        {
            _productRepository.DeleteProduct(productId);
        }
    }
}
