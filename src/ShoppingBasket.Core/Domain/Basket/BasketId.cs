using EventFlow.Core;

namespace ShoppingBasket.Core.Domain.Basket
{
    public class BasketId : 
        Identity<BasketId>
    {
        public BasketId(string value) : base(value) { }
    }
}
