using Discount.Application.Exceptions;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Application.Response;
using Discount.Application.Responses;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetCouponByCodeHandler : IRequestHandler<GetCouponByCodeQuery, BaseResponse>
    {
        private readonly ICouponRepository _couponRepository;

        public GetCouponByCodeHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<BaseResponse> Handle(GetCouponByCodeQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByCodeAsync(request.Code);
            if (coupon == null)
            {
                throw new CouponNotFoundException(request.Code);
            }
            var couponResponse = DiscountMapper.Mapper.Map<CouponDto>(coupon);
            return new BaseResponse(couponResponse);
        }
    }
 
}
