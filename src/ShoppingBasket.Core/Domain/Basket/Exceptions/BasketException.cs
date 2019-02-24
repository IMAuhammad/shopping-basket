using System;

namespace ShoppingBasket.Core.Domain.Basket.Exceptions
{
    class BasketException : Exception
    {
        public BasketException(string exceptionMessage) : base(exceptionMessage)
        {

        }

        public BasketException(string exceptionMessage, Exception innerException) : base(exceptionMessage, innerException)
        {

        }
    }
}
