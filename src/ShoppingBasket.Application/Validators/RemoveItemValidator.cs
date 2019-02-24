using FluentValidation;
using ShoppingBasket.Application.Dtos.Requests;

namespace ShoppingBasket.Application.Validators
{
    public sealed class RemoveItemValidator : AbstractValidator<RemoveItemRequest>
    {
        public RemoveItemValidator()
        {
            RuleFor(b => b.ProductName)
                .NotEmpty()
                .WithErrorCode("901");
        }
    }
}
