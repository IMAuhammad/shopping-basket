using EventFlow.Commands;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Core.Domain.Basket.Models;

namespace ShoppingBasket.Core.Application.Commands
{
    public sealed class AddItem : Command<BasketAggregate, BasketId>
    {
        public AddItem(BasketId aggregateId, string productName, decimal price, int quantity) : base(aggregateId)
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
