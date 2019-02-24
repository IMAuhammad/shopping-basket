using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Core.Domain.Basket.Models
{
    public sealed class BasketItem
    {
        public BasketItem(string productName, decimal price, int quantity)
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        internal BasketItem WithQuantity(int quantity)
        {
            return new BasketItem(ProductName, Price, quantity);
        }
    }
}
