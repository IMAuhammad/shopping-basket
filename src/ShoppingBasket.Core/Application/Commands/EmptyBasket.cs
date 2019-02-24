using EventFlow.Commands;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Core.Domain.Basket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Core.Application.Commands
{
    public sealed class EmptyBasket : Command<BasketAggregate, BasketId>
    {
        public EmptyBasket(BasketId aggregateId) : base(aggregateId)
        {

        }
    }
}
