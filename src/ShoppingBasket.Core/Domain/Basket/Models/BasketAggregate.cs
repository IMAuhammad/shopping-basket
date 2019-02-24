using EventFlow.Aggregates;
using ShoppingBasket.Core.Domain.Basket.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Core.Domain.Basket.Models
{
    public sealed class BasketAggregate : 
        AggregateRoot<BasketAggregate, BasketId>, 
        IEmit<BasketCreated>
    {
        #region Properties

        private Guid _customerId;
        private ICollection<BasketItem> _basketItems = new List<BasketItem>();
        
        #endregion

        #region Constructor

        public BasketAggregate(BasketId id) : base(id) { }
        
        #endregion

        #region Command Processors

        public void CreateCart(Guid customerId)
        {
            if(customerId == Guid.Empty)
            {
                throw new BasketException("customer id should be a valid guid");
            }

            Emit(new BasketCreated(customerId));
        }

        public void AddItem(string productName, decimal price, int quantity)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName), "product name is required");
            }

            if (ContainsItem(productName))
            {
                throw new BasketException($"product {productName} is already in basket");
            }

            ValidateItemQuantity(quantity);

            Emit(new ItemAdded(productName, price, quantity));
        }

        public void AdjustQuantity(string productName, int quantity)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName), "product name is required");
            }

            if (!ContainsItem(productName))
            {
                throw new BasketException($"product {productName} not found in basket");
            }

            ValidateItemQuantity(quantity);

            Emit(new QuantityAdjusted(productName, quantity));
        }

        public void RemoveItem(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName), "product name is required");
            }

            if (!ContainsItem(productName))
            {
                throw new BasketException($"product {productName} not found in basket");
            }

            Emit(new ItemRemoved(productName));
        }

        public void EmptyBasket()
        {
            if(_basketItems.Count > 0)
            {
                Emit(new BasketEmptied());
            }
        }

        #endregion

        #region Apply methods

        public void Apply(BasketCreated aggregateEvent)
        {
            _customerId = aggregateEvent.CustomerId;
        }

        public void Apply(ItemAdded aggregateEvent)
        {
            _basketItems.Add(new BasketItem(aggregateEvent.ProductName, aggregateEvent.Price, aggregateEvent.Quantity));
        }

        public void Apply(QuantityAdjusted aggregateEvent)
        {
            var item = GetBasketItem(aggregateEvent.ProductName);

            _basketItems.Remove(item);
            _basketItems.Add(item.WithQuantity(aggregateEvent.Quantity));
        }

        public void Apply(ItemRemoved aggregateEvent)
        {
            var item = GetBasketItem(aggregateEvent.ProductName);
            _basketItems.Remove(item);
        }

        public void Apply(BasketEmptied aggregateEvent)
        {
            _basketItems.Clear();
        }

        #endregion

        #region Helpers

        private BasketItem GetBasketItem(string productName) => _basketItems.Single(b => b.ProductName == productName);

        private bool ContainsItem(string productName) => _basketItems.Any(b => b.ProductName == productName);

        private void ValidateItemQuantity(int quantity)
        {
            if(quantity <= 0)
            {
                throw new BasketException("quantity value should be greater than zero");
            }
        }

        #endregion
    }
}
