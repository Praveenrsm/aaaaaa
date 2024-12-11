using FarmBusiness.Services;
using FarmTradeEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private CartService _cartService;
        private UserCartService _userCartService;
        public CartController(CartService cartService, UserCartService userCartService)
        {
            _cartService = cartService;
            _userCartService = userCartService;
        }
        [HttpPost("AddCart")]
        public IActionResult AddToCart([FromBody] Cart cart)
        {
            _cartService.AddToCart(cart);
            return Ok(cart);
        }
        [HttpPost("AddUserCart")]
        public IActionResult AddToCart(Guid userId, [FromBody] Cart cart)
        {
            _userCartService.AddToCart(userId, cart);
            return Ok(cart);
        }
        [HttpPut("UpdateQuantity")]
        public IActionResult EditToCart(int productId,int quantity)
        {
            _cartService.UpdateQuantity(productId, quantity);
            return Ok("cart Details updated successfully");
        }
        [HttpPut("UpdateUserQuantity")]
        public IActionResult EditToCart(Guid userId,int productId, int quantity)
        {
            _userCartService.UpdateQuantity(userId,productId, quantity);
            return Ok("cart Details for a user updated successfully");
        }
        [HttpGet("GetCart")]
        public IEnumerable<Cart> GetCarts()
        {
            return _cartService.GetCartDetails();
        }
        [HttpGet("GetUserCart")]
        public IEnumerable<Cart> GetCarts(Guid userId)
        {
            return _userCartService.GetCartItems(userId);
        }
    }
}
