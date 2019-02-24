using EventFlow.Aggregates;
using ShoppingBasket.Core.Domain.Basket.Models;

namespace ShoppingBasket.Core.Domain.Basket.Events
{
    public sealed class ItemRemoved : AggregateEvent<BasketAggregate, BasketId>
    {
        public ItemRemoved(string productName)
        {
            ProductName = productName;
        }

        public string ProductName { get; set; }
    }
}
