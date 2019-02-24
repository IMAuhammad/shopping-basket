namespace ShoppingBasket.Application.Dtos.Responses
{
    using ShoppingBasket.Core.Common;
    using System;
    using System.Collections.Generic;

    public sealed class BasketResponse
    {
        public string BasketId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
        public ICollection<BasketItemResponse> Items { get; set; } = new List<BasketItemResponse>();
    }
}
