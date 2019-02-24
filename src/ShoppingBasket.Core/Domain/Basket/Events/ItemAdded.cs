using EventFlow.Aggregates;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Core.Domain.Basket.Models;

namespace ShoppingBasket.Core.Domain.Basket.Events
{
    public sealed class ItemAdded : AggregateEvent<BasketAggregate, BasketId>
    {
        public ItemAdded(string productName, decimal price, int quantity)
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
