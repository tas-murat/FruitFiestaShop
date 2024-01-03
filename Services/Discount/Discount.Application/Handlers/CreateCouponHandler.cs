using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Application.Response;
using Discount.Application.Responses;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateCouponHandler : IRequestHandler<CreateCouponCommand, BaseResponse>
    {
        private readonly ICouponRepository _couponRepository;

        public CreateCouponHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<BaseResponse> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var couponEntity = DiscountMapper.Mapper.Map<Coupon>(request);
            if (couponEntity is null)
            {
                throw new ApplicationException("There is an issue with mapping while creating new coupon");
            }

            var newCoupon = await _couponRepository.AddAsync(couponEntity);
            var couponResponse= DiscountMapper.Mapper.Map<CouponDto>(newCoupon);
            return new BaseResponse(couponResponse);
        }
    }
}
