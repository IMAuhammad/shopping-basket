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
    class RemoveItemHandler : CommandHandler<BasketAggregate, BasketId, RemoveItem>
    {
        public override Task ExecuteAsync(BasketAggregate aggregate, RemoveItem command, CancellationToken cancellationToken)
        {
            aggregate.RemoveItem(command.ProductName);

            return Task.FromResult(0);
        }
    }
}
