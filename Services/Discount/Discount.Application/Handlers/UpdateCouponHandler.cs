using Discount.Application.Commands;
using Discount.Application.Response;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class UpdateCouponHandler : IRequestHandler<UpdateCouponCommand, BaseResponse>
    {
        private readonly ICouponRepository _couponRepository;

        public UpdateCouponHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<BaseResponse> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = new Coupon
            {
                Id = request.Id,
                CouponCode = request.CouponCode,
                DiscountAmount = request.DiscountAmount,
                MinAmount = request.MinAmount,

            };
            await _couponRepository.UpdateAsync(coupon);
            return new BaseResponse();
        }
    }
}
