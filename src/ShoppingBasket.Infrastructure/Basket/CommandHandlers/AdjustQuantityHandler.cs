using EventFlow.Commands;
using ShoppingBasket.Core.Application.Commands;
using ShoppingBasket.Core.Domain.Basket.Models;
using System.Threading;
using System.Threading.Tasks;
using ShoppingBasket.Core.Domain.Basket;

namespace ShoppingBasket.Infrastructure.Basket.CommandHandlers
{
    public sealed class AdjustQuantityHandler : CommandHandler<BasketAggregate, BasketId, AdjustQuantity>
    {
        public override Task ExecuteAsync(BasketAggregate aggregate, AdjustQuantity command, CancellationToken cancellationToken)
        {
            aggregate.AdjustQuantity(command.ProductName, command.Quantity);

            return Task.FromResult(0);
        }
    }
}
