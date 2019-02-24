using EventFlow.Commands;
using ShoppingBasket.Core.Domain.Basket.Models;
using ShoppingBasket.Core.Domain.Basket;

namespace ShoppingBasket.Core.Application.Commands
{
    public sealed class EmptyBasket : Command<BasketAggregate, BasketId>
    {
        public EmptyBasket(BasketId aggregateId) : base(aggregateId)
        {

        }
    }
}
