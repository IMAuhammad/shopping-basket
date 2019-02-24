using EventFlow.Commands;
using ShoppingBasket.Core.Application.Commands;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Core.Domain.Basket.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingBasket.Infrastructure.Basket.CommandHandlers
{
    class EmptyBasketHandler : CommandHandler<BasketAggregate, BasketId, EmptyBasket>
    {
        public override Task ExecuteAsync(BasketAggregate aggregate, EmptyBasket command, CancellationToken cancellationToken)
        {
            aggregate.EmptyBasket();
            return Task.FromResult(0);
        }
    }
}
