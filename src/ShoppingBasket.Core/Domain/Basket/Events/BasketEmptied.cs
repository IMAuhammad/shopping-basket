using EventFlow.Aggregates;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Core.Domain.Basket.Models;

namespace ShoppingBasket.Core.Domain.Basket.Events
{
    public sealed class BasketEmptied : AggregateEvent<BasketAggregate, BasketId>
    {
        public BasketEmptied()
        {

        }
    }
}
