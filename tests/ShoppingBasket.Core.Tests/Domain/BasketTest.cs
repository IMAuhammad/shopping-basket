using System;
using System.Linq;
using System.Threading;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Queries;
using FluentAssertions;
using ShoppingBasket.Core.Application.Commands;
using ShoppingBasket.Core.Domain.Basket;
using ShoppingBasket.Core.Domain.Basket.Events;
using ShoppingBasket.Infrastructure.Basket.CommandHandlers;
using ShoppingBasket.Infrastructure.Basket.Models;
using Xunit;
using ICommandBus = EventFlow.ICommandBus;

namespace ShoppingBasket.Core.Tests.Domain
{
    public class BasketTest
    {
        private static IEventFlowOptions New
        {
            get
            {
                return EventFlowOptions.New
                    .UseInMemoryReadStoreFor<BasketReadModel>();
            }
        }

        [Fact]
        public void CreateBasketWithValidCustomerId() 
        {
            using (var resolver = New
                .AddEvents(typeof(BasketCreated))
                .AddCommands(typeof(CreateBasket))
                .AddCommandHandlers((typeof(CreateBasketHandler)))
                .CreateResolver())
            {
                var basketId = BasketId.New;

                var customerId = "eb042700-242e-49ee-9a5b-842a9cabb455";

                var commandBus = resolver.Resolve<ICommandBus>();

                Guid customerGuid;

                bool validGuid = Guid.TryParse(customerId, out customerGuid);

                validGuid.Should().BeTrue();
                
                var executionResult = commandBus.Publish(new CreateBasket(basketId, customerGuid), CancellationToken.None);

                executionResult.IsSuccess.Should().BeTrue();

                var queryProcessor = resolver.Resolve<IQueryProcessor>();

                var basketReadModel = queryProcessor.Process(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

                basketReadModel.Should().NotBeNull();
                basketReadModel.BasketId.Should().Be(basketId.Value);
                basketReadModel.CustomerId.Should().Be(customerGuid);
            };
        }

        [Theory]
        [InlineData("eb042700-242e-49ee-9a5b")]
        [InlineData("")]
        [InlineData(null)]
        public void CreateBasketWithInvalidCustomerId(string customerId)
        {
            using (var resolver = New
                .AddEvents(typeof(BasketCreated))
                .AddCommands(typeof(CreateBasket))
                .AddCommandHandlers(typeof(CreateBasketHandler))
                .CreateResolver())
            {
                var basketId = BasketId.New;

                var commandBus = resolver.Resolve<ICommandBus>();

                Guid customerGuid;

                bool validGuid = Guid.TryParse(customerId, out customerGuid);

                validGuid.Should().BeFalse();

                Assert.Throws<AggregateException>(() => commandBus.Publish(new CreateBasket(basketId, customerGuid), CancellationToken.None));
            };
        }

        [Fact]
        public void AddValidItemToBasket()
        {
            using (var resolver = New
                .AddEvents(typeof(BasketCreated), typeof(ItemAdded))
                .AddCommands(typeof(CreateBasket), typeof(AddItem))
                .AddCommandHandlers(typeof(CreateBasketHandler), typeof(AddItemHandler))
                .CreateResolver())
            {
                var basketId = BasketId.New;

                var customerId = Guid.NewGuid();

                var commandBus = resolver.Resolve<ICommandBus>();

                commandBus.Publish(new CreateBasket(basketId, customerId), CancellationToken.None);

                var queryProcessor = resolver.Resolve<IQueryProcessor>();

                var basketReadModel = queryProcessor.Process(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

                basketReadModel.Should().NotBeNull();
                basketReadModel.BasketId.Should().Be(basketId.Value);
                basketReadModel.CustomerId.Should().Be(customerId);
                basketReadModel.BasketItems.Count.Should().Be(0);
                
                commandBus.Publish(new AddItem(basketId, "Product 1", 1000m, 1), CancellationToken.None);

                basketReadModel.BasketItems.Count.Should().Be(1);
                basketReadModel.BasketItems.First().Should().NotBeNull();
                basketReadModel.BasketItems.First().ProductName.Should().BeEquivalentTo("Product 1");
                basketReadModel.BasketItems.First().Price.Should().Be(1000m);
                basketReadModel.BasketItems.First().Quantity.Should().Be(1);
            };
        }

        [Theory]
        [InlineData(null, 10, 1)]
        [InlineData("Product", null, 0)]
        [InlineData("", 0, 0)]
        [InlineData(null, 0, 0)]
        [InlineData("Product", 100, 0)]
        [InlineData("Product", 100, null)]
        public void AddInvalidItemToBasket(string productName, decimal price, int quantity)
        {
            using (var resolver = New
                .AddEvents(typeof(BasketCreated), typeof(ItemAdded))
                .AddCommands(typeof(CreateBasket), typeof(AddItem))
                .AddCommandHandlers(typeof(CreateBasketHandler), typeof(AddItemHandler))
                .CreateResolver())
            {
                var basketId = BasketId.New;

                var customerId = Guid.NewGuid();

                var commandBus = resolver.Resolve<ICommandBus>();

                commandBus.Publish(new CreateBasket(basketId, customerId), CancellationToken.None);

                var queryProcessor = resolver.Resolve<IQueryProcessor>();

                var basketReadModel = queryProcessor.Process(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

                basketReadModel.Should().NotBeNull();
                basketReadModel.BasketId.Should().Be(basketId.Value);
                basketReadModel.CustomerId.Should().Be(customerId);
                basketReadModel.BasketItems.Count.Should().Be(0);
                
                Assert.Throws<AggregateException>(() => commandBus.Publish(new AddItem(basketId, productName, price, quantity), CancellationToken.None));
            };
        }

        [Fact]
        public void AdjustQuantityOfBasketItemWithValidQuantity()
        {
            using (var resolver = New
                .AddEvents(typeof(BasketCreated), typeof(ItemAdded), typeof(QuantityAdjusted))
                .AddCommands(typeof(CreateBasket), typeof(AddItem), typeof(AdjustQuantity))
                .AddCommandHandlers(typeof(CreateBasketHandler), typeof(AddItemHandler), typeof(AdjustQuantityHandler))
                .CreateResolver())
            {
                var basketId = BasketId.New;

                var customerId = Guid.NewGuid();

                var commandBus = resolver.Resolve<ICommandBus>();

                commandBus.Publish(new CreateBasket(basketId, customerId), CancellationToken.None);
                commandBus.Publish(new AddItem(basketId, "Product 1", 1000m, 1), CancellationToken.None);

                var queryProcessor = resolver.Resolve<IQueryProcessor>();

                var basketReadModel = queryProcessor.Process(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

                basketReadModel.BasketItems.Count.Should().Be(1);
                basketReadModel.BasketItems.First().ProductName.Should().BeEquivalentTo("Product 1");
                basketReadModel.BasketItems.First().Quantity.Should().Be(1);

                var executionResult =
                    commandBus.Publish(new AdjustQuantity(basketId, "Product 1", 3), CancellationToken.None);

                executionResult.IsSuccess.Should().BeTrue();

                basketReadModel = queryProcessor.Process(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

                basketReadModel.BasketItems.Count.Should().Be(1);
                basketReadModel.BasketItems.First().ProductName.Should().BeEquivalentTo("Product 1");
                basketReadModel.BasketItems.First().Quantity.Should().Be(3);
            };
        }

        [Theory]
        [InlineData("Product 1", -1)]
        [InlineData("Product 1", null)]
        [InlineData("Product 1", 0)]
        public void AdjustQuantityOfBasketItemWithInvalidQuantity(string productName, int quantity)
        {
            using (var resolver = New
                .AddEvents(typeof(BasketCreated), typeof(ItemAdded), typeof(QuantityAdjusted))
                .AddCommands(typeof(CreateBasket), typeof(AddItem), typeof(AdjustQuantity))
                .AddCommandHandlers(typeof(CreateBasketHandler), typeof(AddItemHandler), typeof(AdjustQuantityHandler))
                .CreateResolver())
            {
                var basketId = BasketId.New;

                var customerId = Guid.NewGuid();

                var commandBus = resolver.Resolve<ICommandBus>();

                commandBus.Publish(new CreateBasket(basketId, customerId), CancellationToken.None);
                commandBus.Publish(new AddItem(basketId, productName, 1000m, 10), CancellationToken.None);

                var queryProcessor = resolver.Resolve<IQueryProcessor>();

                var basketReadModel = queryProcessor.Process(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

                basketReadModel.BasketItems.Count.Should().Be(1);
                basketReadModel.BasketItems.First().ProductName.Should().BeEquivalentTo(productName);
                basketReadModel.BasketItems.First().Quantity.Should().Be(10);

                Assert.Throws<AggregateException>(() => commandBus.Publish(new AdjustQuantity(basketId, productName, quantity), CancellationToken.None));
            };
        }

        [Fact]
        public void EmptyBasketOfItems()
        {
            using (var resolver = New
                .AddEvents(typeof(BasketCreated), typeof(ItemAdded), typeof(BasketEmptied))
                .AddCommands(typeof(CreateBasket), typeof(AddItem), typeof(EmptyBasket))
                .AddCommandHandlers(typeof(CreateBasketHandler), typeof(AddItemHandler), typeof(EmptyBasketHandler))
                .CreateResolver())
            {
                var basketId = BasketId.New;

                var customerId = Guid.NewGuid();

                var commandBus = resolver.Resolve<ICommandBus>();
                
                commandBus.Publish(new CreateBasket(basketId, customerId), CancellationToken.None);
                commandBus.Publish(new AddItem(basketId, "Product 1", 1000m, 1), CancellationToken.None);
                commandBus.Publish(new AddItem(basketId, "Product 2", 1000m, 1), CancellationToken.None);

                var queryProcessor = resolver.Resolve<IQueryProcessor>();

                var basketReadModel = queryProcessor.Process(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

                basketReadModel.Should().NotBeNull();
                basketReadModel.BasketId.Should().Be(basketId.Value);
                basketReadModel.CustomerId.Should().Be(customerId);
                basketReadModel.BasketItems.Count.Should().Be(2);

                var executionResult = commandBus.Publish(new EmptyBasket(basketId), CancellationToken.None);

                executionResult.IsSuccess.Should().BeTrue();

                //Basket readmodel has no BasketItems
                basketReadModel = queryProcessor.Process(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

                basketReadModel.BasketItems.Count.Should().Be(0);
            };
        }
        
    }
}
