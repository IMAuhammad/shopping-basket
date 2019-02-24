using EventFlow.Commands;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Core.Domain.Basket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Core.Application.Commands
{
    public class RemoveItem : Command<BasketAggregate, BasketId>
    {
        public RemoveItem(BasketId aggregateId, string productName) : base(aggregateId)
        {
            ProductName = productName;
        }

        public string ProductName { get; set; }
    }
}
