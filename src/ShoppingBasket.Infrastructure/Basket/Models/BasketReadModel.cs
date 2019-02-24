using EventFlow.Aggregates;
using EventFlow.ReadStores;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Core.Domain.Basket.Events;
using ShoppingBasket.Core.Domain.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Infrastructure.Basket.Models
{
    public sealed class BasketReadModel : IReadModel, 
        IAmReadModelFor<BasketAggregate, BasketId, BasketCreated>, 
        IAmReadModelFor<BasketAggregate, BasketId, ItemAdded>,
        IAmReadModelFor<BasketAggregate, BasketId, QuantityAdjusted>,
        IAmReadModelFor<BasketAggregate, BasketId, ItemRemoved>,
        IAmReadModelFor<BasketAggregate, BasketId, BasketEmptied>
    {
        public string BasketId { get; private set; }
        public Guid CustomerId { get; private set; }
        public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

        public void Apply(IReadModelContext context, IDomainEvent<BasketAggregate, BasketId, BasketCreated> domainEvent)
        {
            BasketId = domainEvent.AggregateIdentity.Value;
            CustomerId = domainEvent.AggregateEvent.CustomerId;
        }

        public void Apply(IReadModelContext context, IDomainEvent<BasketAggregate, BasketId, ItemAdded> domainEvent)
        {
            BasketItems.Add(new BasketItem(domainEvent.AggregateEvent.ProductName, domainEvent.AggregateEvent.Price, domainEvent.AggregateEvent.Quantity));
        }

        public void Apply(IReadModelContext context, IDomainEvent<BasketAggregate, BasketId, QuantityAdjusted> domainEvent)
        {
            var item = GetBasketItem(domainEvent.AggregateEvent.ProductName);

            if (item != null)
            {
                item.Quantity = domainEvent.AggregateEvent.Quantity;
            }
        }

        public void Apply(IReadModelContext context, IDomainEvent<BasketAggregate, BasketId, ItemRemoved> domainEvent)
        {
            var item = GetBasketItem(domainEvent.AggregateEvent.ProductName);

            if (item != null)
            {
                BasketItems.Remove(item);
            }
        }

        public void Apply(IReadModelContext context, IDomainEvent<BasketAggregate, BasketId, BasketEmptied> domainEvent)
        {
            BasketItems.Clear();
        }

        #region Helpers

        private BasketItem GetBasketItem(string productName)
        {
            return BasketItems.SingleOrDefault(b => b.ProductName == productName);
        }

        #endregion
    }
}
