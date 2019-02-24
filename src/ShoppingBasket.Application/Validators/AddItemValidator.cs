using FluentValidation;
using ShoppingBasket.Application.Dtos.Requests;

namespace ShoppingBasket.Application.Validators
{
    public sealed class AddItemValidator : AbstractValidator<AddItemRequest>
    {
        public AddItemValidator()
        {
            RuleFor(b => b.ProductName)
                .NotEmpty()
                .WithMessage("Product name is required")
                .WithErrorCode("901");
            RuleFor(b => b.Price)
                .NotEmpty()
                .WithMessage("Product price is required")
                .WithErrorCode("902");
            RuleFor(b => b.Quantity)
                .GreaterThan(0)
                .WithMessage("Product quantity should be greater than zero")
                .WithErrorCode("903");
        }
    }
}
