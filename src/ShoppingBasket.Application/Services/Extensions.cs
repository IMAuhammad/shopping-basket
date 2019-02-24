using ShoppingBasket.Application.Dtos.Responses;
using ShoppingBasket.Core.Common;
using ShoppingBasket.Infrastructure.Basket.Models;
using System.Linq;

namespace ShoppingBasket.Application.Services
{
    public static class Extensions
    {
        public static BasketResponse ToBasketResponse(this BasketReadModel basketReadModel)
        {
            var response = new BasketResponse
            {
                BasketId = basketReadModel.BasketId,
                CustomerId = basketReadModel.CustomerId,
                TotalPrice = basketReadModel.BasketItems.Sum(b => b.Price * b.Quantity),
                TotalItems = basketReadModel.BasketItems.Sum(b => b.Quantity)
            };

            foreach(var item in basketReadModel.BasketItems)
            {
                response.Items.Add(new BasketItemResponse
                {
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

            return response;
        }
    }
}
