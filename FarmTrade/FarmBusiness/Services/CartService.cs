using FarmTradeDataLayer.Repository;
using FarmTradeEntity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmBusiness.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        ICartRepo _cartRepo;
        public CartService(ICartRepo cartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepo = cartRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public  void AddToCart(Cart cartItem)
        {
            _cartRepo.AddToCart(cartItem);
        }
        private Guid GetOrCreateSessionId()
        {
            // Retrieve the session ID from cookies
            var sessionIdCookie = _httpContextAccessor.HttpContext?.Request.Cookies["SessionId"];

            // If the session ID exists in the cookie, return it
            if (!string.IsNullOrEmpty(sessionIdCookie) && Guid.TryParse(sessionIdCookie, out Guid existingSessionId))
            {
                return existingSessionId;
            }

            // If not, create a new session ID
            var newSessionId = Guid.NewGuid();

            // Store the new session ID in a cookie
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("SessionId", newSessionId.ToString(), new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1) // Set expiration as needed
            });

            return newSessionId;

        }
        public IEnumerable<Cart> GetCartDetails()
        {
            return _cartRepo.GetCartDetails();
        }
        public void UpdateQuantity(int productId,int Quantity)
        {
            _cartRepo.UpdateQuantity(productId,Quantity); 
        }
    }
}
