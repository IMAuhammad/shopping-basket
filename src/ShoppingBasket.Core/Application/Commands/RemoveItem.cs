using EventFlow.Commands;
using ShoppingBasket.Core.Domain.Basket.Models;
using ShoppingBasket.Core.Domain.Basket;

namespace ShoppingBasket.Core.Application.Commands
{
    public sealed class RemoveItem : Command<BasketAggregate, BasketId>
    {
        public RemoveItem(BasketId aggregateId, string productName) : base(aggregateId)
        {
            ProductName = productName;
        }

        public string ProductName { get; set; }
    }
}
