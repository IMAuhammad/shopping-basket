using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using ShoppingBasket.Core.Application.Commands;
using ShoppingBasket.Core.Domain.Basket;
using ShoppingBasket.Core.Domain.Basket.Models;

namespace ShoppingBasket.Infrastructure.Basket.CommandHandlers
{
    public sealed class CreateBasketHandler : CommandHandler<BasketAggregate, BasketId, CreateBasket>
    {
        public override Task ExecuteAsync(BasketAggregate aggregate, CreateBasket command, CancellationToken cancellationToken)
        {
            aggregate.CreateCart(command.CustomerId);
            return Task.FromResult(0);
        }
    }
}
