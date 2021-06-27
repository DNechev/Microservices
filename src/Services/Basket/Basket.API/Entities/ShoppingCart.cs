using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }

        public ShoppingCart(string username) 
            : this()
        {
            this.Username = username;
        }

        public string Username { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
