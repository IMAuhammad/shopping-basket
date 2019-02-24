using System;

namespace ShoppingBasket.Core.Domain.Basket
{
    public sealed class BasketException : Exception
    {
        public BasketException(string exceptionMessage) : base(exceptionMessage)
        {

        }

        public BasketException(string exceptionMessage, Exception innerException) : base(exceptionMessage, innerException)
        {

        }
    }
}
