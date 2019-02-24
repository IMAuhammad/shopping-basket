using EventFlow.Commands;
using ShoppingBasket.Core.Domain.Basket.Models;
using ShoppingBasket.Core.Domain.Basket;

namespace ShoppingBasket.Core.Application.Commands
{
    public sealed class AdjustQuantity : Command<BasketAggregate, BasketId>
    {
        public AdjustQuantity(BasketId aggregateId, string productName, int quantity) : base(aggregateId)
        {
            ProductName = productName;
            Quantity = quantity;
        }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
