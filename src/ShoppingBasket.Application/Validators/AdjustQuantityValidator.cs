using FluentValidation;
using ShoppingBasket.Application.Dtos.Requests;

namespace ShoppingBasket.Application.Validators
{
    public sealed class AdjustQuantityValidator : AbstractValidator<AdjustQuantityRequest>
    {
        public AdjustQuantityValidator()
        {
            RuleFor(b => b.ProductName)
                .NotEmpty()
                .WithMessage("Product name is required")
                .WithErrorCode("901");
            RuleFor(b => b.Quantity)
                .NotEmpty()
                .WithErrorCode("902");
        }
    }
}
