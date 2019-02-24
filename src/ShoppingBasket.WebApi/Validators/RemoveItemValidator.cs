using FluentValidation;
using ShoppingBasket.Application.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.WebApi.Validators
{
    public class RemoveItemValidator : AbstractValidator<RemoveItemRequest>
    {
        public RemoveItemValidator()
        {
            RuleFor(b => b.ProductName)
                .NotEmpty()
                .WithErrorCode("901");
        }
    }
}
