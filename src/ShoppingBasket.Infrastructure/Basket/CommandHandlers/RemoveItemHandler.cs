using EventFlow.Commands;
using ShoppingBasket.Core.Application.Commands;
using ShoppingBasket.Core.Domain.Basket.Models;
using System.Threading;
using System.Threading.Tasks;
using ShoppingBasket.Core.Domain.Basket;

namespace ShoppingBasket.Infrastructure.Basket.CommandHandlers
{
    public sealed class RemoveItemHandler : CommandHandler<BasketAggregate, BasketId, RemoveItem>
    {
        public override Task ExecuteAsync(BasketAggregate aggregate, RemoveItem command, CancellationToken cancellationToken)
        {
            aggregate.RemoveItem(command.ProductName);

            return Task.FromResult(0);
        }
    }
}
