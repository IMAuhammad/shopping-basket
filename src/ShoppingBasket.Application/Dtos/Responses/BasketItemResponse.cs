namespace ShoppingBasket.Application.Dtos.Responses
{
    public sealed class BasketItemResponse
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
