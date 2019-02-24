using System;
namespace ShoppingBasket.Application.Dtos.Requests
{
    public sealed class CreateBasketRequest
    {
        public Guid CustomerId { get; set; }
    }
}
