using FarmTradeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeDataLayer.Repository
{
    public interface ICartRepo
    {
        void AddToCart(Cart cartItem);
        IEnumerable<Cart> GetCartDetails();
        void UpdateQuantity(int productId,int Quantity);
    }
}
