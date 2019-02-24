using EventFlow.Commands;
using ShoppingBasket.Core.Application.Commands;
using ShoppingBasket.Core.Domain.Basket.Models;
using System.Threading;
using System.Threading.Tasks;
using ShoppingBasket.Core.Domain.Basket;

namespace ShoppingBasket.Infrastructure.Basket.CommandHandlers
{
    public sealed class AddItemHandler : CommandHandler<BasketAggregate, BasketId, AddItem>
    {
        public override Task ExecuteAsync(BasketAggregate aggregate, AddItem command, CancellationToken cancellationToken)
        {
            aggregate.AddItem(command.ProductName, command.Price, command.Quantity);
            return Task.FromResult(0);
        }
    }
}
