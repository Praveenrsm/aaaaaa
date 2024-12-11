using FarmTradeDataLayer;
using FarmTradeEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmBusiness.Services
{
    public class UserCartService
    {
        private readonly FarmContext _dbContext;

        public UserCartService(FarmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddToCart(Guid userId, Cart cartItem)
        {
            // Find the product details in the database
            var product = _dbContext.product.FirstOrDefault(p => p.ProductId == cartItem.productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var existingItem = _dbContext.cart
                .FirstOrDefault(item => item.userId == userId && item.productId == cartItem.productId);
            decimal productPrice = product.productPrice;
            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity > 0 ? cartItem.Quantity : 1;
                existingItem.totalPrice = existingItem.Quantity * productPrice;
                _dbContext.cart.Update(existingItem);
            }
            else
            {
                cartItem.userId = userId;
                cartItem.Quantity = cartItem.Quantity > 0 ? cartItem.Quantity : 1;
                cartItem.totalPrice = cartItem.Quantity * productPrice; // Calculate totalPrice with decimal
                cartItem.product = product; // Link the product for relational mapping
                _dbContext.cart.Add(cartItem);
                //cartItem.userId = userId;
                //_dbContext.cart.Add(cartItem);
            }
            _dbContext.SaveChanges();
        }

        public List<Cart> GetCartItems(Guid userId)
        {
            return _dbContext.cart
                .Where(item => item.userId == userId)
                .ToList();
        }

        public void UpdateQuantity(Guid userId,int productId, int newQuantity)
        {
            var cartProducts = _dbContext.cart
                .Include(c => c.product)
                .FirstOrDefault(item => item.productId == productId && item.userId == userId);
            if (cartProducts == null)
            {
                throw new Exception("Cart for the Product not found");
            }


            if (newQuantity == 0)
            {
                // Remove item from cart if quantity is zero
                _dbContext.cart.Remove(cartProducts);
            }
            else
            {
                // Update item with new quantity and calculate total price
                decimal productPrice = cartProducts.product.productPrice;
                cartProducts.Quantity = newQuantity;
                cartProducts.totalPrice = newQuantity * productPrice;
                _dbContext.cart.Update(cartProducts);
            }

            _dbContext.SaveChanges();
        }
    }

}
