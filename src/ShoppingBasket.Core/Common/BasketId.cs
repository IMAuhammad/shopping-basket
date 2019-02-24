namespace ShoppingBasket.Core.Common
{
    using EventFlow.Core;

    public class BasketId : 
        Identity<BasketId>
    {
        public BasketId(string value) : base(value) { }
    }
}
