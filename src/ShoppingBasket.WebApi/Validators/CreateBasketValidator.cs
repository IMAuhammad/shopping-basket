﻿using FluentValidation;
using ShoppingBasket.Application.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.WebApi.Validators
{
    public class CreateBasketValidator : AbstractValidator<CreateBasketRequest>
    {
        public CreateBasketValidator()
        {
            RuleFor(b => b.CustomerId)
                .NotEmpty()
                .WithMessage("Customer ID should be a valid guid e.g. eb042700-242e-49ee-9a5b-842a9cabb454")
                .WithErrorCode("900");
        }
    }
}
