using EventFlow.Commands;
using ShoppingBasket.Core.Domain.Basket.Models;
using System;
using ShoppingBasket.Core.Domain.Basket;

namespace ShoppingBasket.Core.Application.Commands
{
    public sealed class CreateBasket : Command<BasketAggregate, BasketId>
    {
        public CreateBasket(BasketId aggregateId, Guid customerId) : base(aggregateId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}
