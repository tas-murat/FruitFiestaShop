using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Application.Response;
using Discount.Application.Responses;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetAllCouponHandler : IRequestHandler<GetAllCouponQuery, BaseResponse>
    {
        private readonly ICouponRepository _couponRepository;

        public GetAllCouponHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<BaseResponse> Handle(GetAllCouponQuery request, CancellationToken cancellationToken)
        {
            var coupons = await _couponRepository.GetAllAsync();
            var couponResponses = DiscountMapper.Mapper.Map<IList<CouponDto>>(coupons);
            return new BaseResponse(couponResponses);
        }
    }

}
