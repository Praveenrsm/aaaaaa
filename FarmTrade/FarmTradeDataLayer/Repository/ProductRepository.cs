using FarmTradeEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeDataLayer.Repository
{
    public class ProductRepository:IproductRepository
    {
        FarmContext _farmcontext;
        public ProductRepository(FarmContext context)
        {
            _farmcontext = context;
        }

        public string AddProduct(Product product, List<byte[]> imageList)
        {
            var user = _farmcontext.users.FirstOrDefault(u => u.UserId == product.userId);
            if (user == null)
            {
                return "User not found";
            }

            if (user.role == "supplier" || user.role == "admin")
            {
                // Process each image in the list, possibly resizing/compressing here
                foreach (var imageData in imageList)
                {
                    // Optionally, resize or compress the image data here before storing
                    var compressedImageData = CompressImage(imageData); // Implement a compression method

                    product.ProductImages.Add(new ProductImage { ImageData = compressedImageData });
                }

                _farmcontext.product.Add(product);
                _farmcontext.SaveChanges();

                // Return a response with essential details only, excluding image data
                return $"Product '{product.ProductName}' added successfully with {imageList.Count} images";
            }
            else
            {
                return "You are not a Supplier/admin";
            }
        }

        // Helper function to compress images (optional)
        private byte[] CompressImage(byte[] imageData)
        {
            // Implement compression logic, e.g., using System.Drawing or a library like SkiaSharp
            // For demonstration, returning original data
            return imageData;
        }

        public void UpdateProduct(Product product, List<byte[]> newImageList)
        {
            var existingProduct = _farmcontext.product
                .Include(p => p.ProductImages) // Load existing images
                .FirstOrDefault(p => p.ProductId == product.ProductId);

            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }

            // Update basic product fields
            existingProduct.ProductName = product.ProductName;
            existingProduct.productPrice = product.productPrice;
            existingProduct.Description = product.Description;
            existingProduct.availableQuantity = product.availableQuantity;

            // Update or add images
            existingProduct.ProductImages.Clear(); // Clear old images, if necessary
            foreach (var imageData in newImageList)
            {
                var compressedImageData = CompressImage(imageData); // Compress if needed
                existingProduct.ProductImages.Add(new ProductImage { ImageData = compressedImageData });
            }

            _farmcontext.SaveChanges();
        }

        //public void UpdateProduct(Product product)
        //{
        //    #region EDIT PRODUCT
        //    _farmcontext.Entry(product).State = EntityState.Modified;
        //    _farmcontext.SaveChanges();
        //    #endregion
        //}

        public Product GetProductWithReviews(int productId)
        {
            #region GET Single product 
            //var result = _farmcontext.product.Include(obj => obj.User).ToList();
            //var product = result.Where(obj => obj.ProductId == productId).FirstOrDefault();
            //return _farmcontext.product
            //.Include(p => p.Reviews)
            //.ThenInclude(r => r.User)
            //.FirstOrDefault(p => p.ProductId == productId);
                return _farmcontext.Set<Product>()
                    .Where(p => p.ProductId == productId)
                    .Include(p => p.Reviews)
                    .ThenInclude(r => r.User)
                    .Include(p => p.ProductImages)
                    .Select(p => new Product
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        //productImage = p.productImage,
                        productPrice = p.productPrice,
                        Description = p.Description,
                        availableQuantity = p.availableQuantity,
                        userId = p.userId,
                        User = p.User,
                        Reviews = p.Reviews.Select(r => new ReviewsAndRatings
                        {
                            Id = r.Id,
                            Comments = r.Comments,
                            Rating = r.Rating,
                            User = new User
                            {
                                UserName = r.User.UserName
                            }
                        }).ToList(),
                        ProductImages = p.ProductImages.Where(r => r.ProductId == productId).ToList(),
                    })
                    .FirstOrDefault();
            #endregion
        }

        //public IEnumerable<Product> GetAllProducts()
        //{
        //    #region GET All PRODUCTS
        //    return _farmcontext.product.Include(obj => obj.User).Include(obj => obj.Reviews).ToList();
        //    #endregion
        //}
        public IEnumerable<Product> GetAllProducts()
        {
            #region GET All PRODUCTS
            return _farmcontext.product
                .Include(p => p.User)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .Select(p => new Product
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    productPrice = p.productPrice,
                    Description = p.Description,
                    availableQuantity = p.availableQuantity,
                    userId = p.userId,
                    User = p.User,
                    Reviews = p.Reviews,
                    ProductImages = p.ProductImages
                        .OrderBy(pi => pi.Id) // Order to ensure consistency
                        .Take(1) // Only take the first image
                        .ToList()
                })
                .ToList();
            #endregion
        }

        public void DeleteProduct(int productId)
        {
            #region DELETE PRODUCT
            var product = _farmcontext.product.Find(productId);
            _farmcontext.product.Remove(product);
            _farmcontext.SaveChanges();
            #endregion
        }
    }
}
