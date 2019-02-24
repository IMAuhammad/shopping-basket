using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Application.Dtos.Requests;
using ShoppingBasket.Application.Dtos.Responses;
using ShoppingBasket.Application.Services;
using ShoppingBasket.Core.Application.Commands;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Infrastructure.Basket.Models;
using ShoppingBasket.WebApi.Validators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingBasket.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BasketController : ControllerBase
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public BasketController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        // POST /api/v1/basket
        [HttpPost(Name = "CreateBasket")]
        public async Task<IActionResult> Create([FromBody] CreateBasketRequest request)
        {
            var validation = new CreateBasketValidator().Validate(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var basketId = BasketId.New;

            var task = await commandBus.PublishAsync(new CreateBasket(basketId, request.CustomerId), CancellationToken.None);

            if (task.IsSuccess)
            {
                return CreatedAtRoute("GetBasket", new { basketId = basketId.ToString() }, basketId.ToString() );
            }

            return BadRequest();
        }

        // POST /api/v1/basket/{basketId}
        [HttpPost("{basketId}", Name = "AddItem")]
        public async Task<IActionResult> AddItem(string basketId, [FromBody] AddItemRequest request)
        {
            var validation = new AddItemValidator().Validate(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var basketReadModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

            if(basketReadModel == null)
            {
                return NotFound();
            }

            var task = await commandBus.PublishAsync(
                new AddItem(new BasketId(basketId), request.ProductName, request.Price, request.Quantity), CancellationToken.None);

            if (task.IsSuccess)
            {
                return CreatedAtRoute("GetBasket", new { basketId = basketId.ToString() }, basketId.ToString());
            }

            return BadRequest();
        }

        // PUT /api/v1/basket/{basketId}
        [HttpPut("{basketId}", Name = "UpdateItemQuantity")]
        public async Task<IActionResult> UpdateQuantity(string basketId, [FromBody] AdjustQuantityRequest request)
        {
            var validation = new AdjustQuantityValidator().Validate(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var basketReadModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

            if (basketReadModel == null)
            {
                return NotFound();
            }

            var task = await commandBus.PublishAsync(
                new AdjustQuantity(new BasketId(basketId), request.ProductName, request.Quantity), CancellationToken.None);

            if (task.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest();
        }
        
        // DELETE /api/v1/basket/{basketId}
        [HttpDelete("{basketId}", Name = "RemoveItem")]
        public async Task<IActionResult> RemoveItem(string basketId, [FromBody] RemoveItemRequest request)
        {
            var validation = new RemoveItemValidator().Validate(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var basketReadModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

            if (basketReadModel == null)
            {
                return NotFound();
            }

            var task = await commandBus.PublishAsync(
                new RemoveItem(new BasketId(basketId), request.ProductName), CancellationToken.None);

            if (task.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest();
        }
        
        // DELETE /api/v1/basket/{basketId}/clear
        [HttpDelete("{basketId}/clear", Name = "EmptyBasket")]
        public async Task<IActionResult> EmptyBasket(string basketId)
        {
            var basketReadModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

            if (basketReadModel == null)
            {
                return NotFound();
            }

            var task = await commandBus.PublishAsync(
                new EmptyBasket(new BasketId(basketId)), CancellationToken.None);

            if (task.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest();
        }
        
        // GET /api/v1/basket/{basketId}
        [HttpGet("{basketId}", Name = "GetBasket")]
        public async Task<ActionResult<BasketResponse>> Get(string basketId)
        {
            var response = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<BasketReadModel>(basketId), CancellationToken.None);

            if (response == null)
            {
                return NotFound();
            }

            var basketResponse = response.ToBasketResponse();

            return Ok(basketResponse);
        }
    }
}