using EventFlow.Commands;
using ShoppingBasket.Core.Application.Commands;
using ShoppingBasket.Core.Domain.Basket.Models;
using System.Threading;
using System.Threading.Tasks;
using ShoppingBasket.Core.Domain.Basket;

namespace ShoppingBasket.Infrastructure.Basket.CommandHandlers
{
    public sealed class EmptyBasketHandler : CommandHandler<BasketAggregate, BasketId, EmptyBasket>
    {
        public override Task ExecuteAsync(BasketAggregate aggregate, EmptyBasket command, CancellationToken cancellationToken)
        {
            aggregate.EmptyBasket();
            return Task.FromResult(0);
        }
    }
}
