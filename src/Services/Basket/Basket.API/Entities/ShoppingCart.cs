using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.ShoppingCartItems = new List<ShoppingCartItem>();
        }

        public ShoppingCart(string username) 
            : this()
        {
            this.Username = username;
        }

        public string Username { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public decimal TotalPrice {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in ShoppingCartItems)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
