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
    class AddItemHandler : CommandHandler<BasketAggregate, BasketId, AddItem>
    {
        public override Task ExecuteAsync(BasketAggregate aggregate, AddItem command, CancellationToken cancellationToken)
        {
            aggregate.AddItem(command.ProductName, command.Price, command.Quantity);
            return Task.FromResult(0);
        }
    }
}
