using EventFlow.Aggregates;
using ShoppingBasket.Core.Domain.Basket.Models;
using System;

namespace ShoppingBasket.Core.Domain.Basket.Events
{
    public sealed class BasketCreated : AggregateEvent<BasketAggregate, BasketId>
    {
        public BasketCreated(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}
