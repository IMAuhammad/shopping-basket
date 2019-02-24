using EventFlow.Aggregates;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Core.Domain.Basket.Models;

namespace ShoppingBasket.Core.Domain.Basket.Events
{
    public sealed class QuantityAdjusted : AggregateEvent<BasketAggregate, BasketId>
    {
        public QuantityAdjusted(string productName, int quantity)
        {
            ProductName = productName;
            Quantity = quantity;
        }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
