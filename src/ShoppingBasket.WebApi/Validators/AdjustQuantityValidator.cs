using FluentValidation;
using ShoppingBasket.Application.Dtos.Requests;

namespace ShoppingBasket.WebApi.Validators
{
    public class AdjustQuantityValidator : AbstractValidator<AdjustQuantityRequest>
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
