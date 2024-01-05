using Discount.Application.Commands;
using FluentValidation;

namespace Discount.Application.Validators
{
    public class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
    {
        public CreateCouponCommandValidator()
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
            
           
        }
    }
}
