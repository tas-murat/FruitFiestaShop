using Discount.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Validators
{
    public class UpdateCouponCommandValidator : AbstractValidator<UpdateCouponCommand>
    {
        public UpdateCouponCommandValidator()
        {
            RuleFor(o => o.CouponCode)
                .NotEmpty()
                .WithMessage("{CouponCode} zorunludur")
                .NotNull()
                .MaximumLength(50)
                .WithMessage("{DiscountAmount} en fazla 50 karekter olmalı");
            RuleFor(o => o.DiscountAmount)
                .NotEmpty()
                .WithMessage("{DiscountAmount} zorunludur.")
                .GreaterThan(0)
                .WithMessage("{DiscountAmount} 0 dan büyük olmalıdır");
            RuleFor(o => o.MinAmount)
                .NotEmpty()
                .WithMessage("{MinAmount} zorunludur")
                .GreaterThan(0)
                .WithMessage("{MinAmount} 0 dan büyük olmalıdır");
            
            RuleFor(o => o.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Id} update işleminde zorunludur");
        }
    }
}
