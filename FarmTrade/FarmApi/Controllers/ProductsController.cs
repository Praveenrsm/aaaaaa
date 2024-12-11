using FarmApi.Model;
using FarmBusiness.Services;
using FarmTradeDataLayer;
using FarmTradeEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FarmApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductService _productService;
        FarmContext _farmcontext;
        public ProductsController(ProductService productService, FarmContext farmcontext)
        {
            _productService = productService;
            _farmcontext = farmcontext;
        }
        //[HttpPost("upload")]
        //public IActionResult UploadImage([FromForm] files file,int productId)
        //{
        //    if (file == null || file.image.Length == 0)
        //        return BadRequest("File is not selected");

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        // Copy the file to memory stream without async
        //        file.image.CopyTo(memoryStream);

        //        // Create the ProductImage entity and set the image data
        //        var imageEntity = new ProductImage
        //        {
        //            ImageData = memoryStream.ToArray(), // Store as byte[]
        //            ProductId = productId
        //        };

        //        // Add the image entity to the context and save changes synchronously
        //        _farmcontext.ProductImage.Add(imageEntity);
        //        _farmcontext.SaveChanges();

        //        return Ok("Image uploaded successfully");
        //    }
        //}
        [AllowAnonymous]
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] ProductCreationModel product)
        {
            #region Edit PRODUCT:
            var imageBytesList = product.ImageList.Select(image => Convert.FromBase64String(image)).ToList();
            _productService.UpdateProduct(product.Product, imageBytesList);
            return Ok("Product Details updated successfully");
            #endregion
        }
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] ProductCreationModel model)
        {
            #region Adding Products
            if (model == null || model.Product == null || model.ImageList == null || !model.ImageList.Any())
            {
                return BadRequest("Invalid product data or images.");
            }
            var imageBytesList = model.ImageList.Select(image => Convert.FromBase64String(image)).ToList();
            var result = _productService.AddProduct(model.Product, imageBytesList);
            if (result == "Product added successfully with multiple images")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
            #endregion
        }

        [AllowAnonymous]
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProductWithReviews(int productId)
        {
            var product = _productService.GetProductWithReviews(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [AllowAnonymous]
        [HttpGet("GetAllProducts")]
        public IEnumerable<Product> GetAllProducts()
        {
            #region Get Product
            return _productService.GetAllProducts();
            #endregion
        }
        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            #region Delete User
            _productService.DeleteProduct(productId);
            return Ok("Product deleted successfully!!!");
            #endregion
        }
    }
}
