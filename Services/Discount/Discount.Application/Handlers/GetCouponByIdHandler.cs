using Discount.Application.Exceptions;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Application.Response;
using Discount.Application.Responses;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetCouponByIdHandler : IRequestHandler<GetCouponByIdQuery, BaseResponse>
    {
        private readonly ICouponRepository _couponRepository;

        public GetCouponByIdHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<BaseResponse> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByIdAsync(request.Id);
            if (coupon == null)
            {
                throw new CouponNotFoundException(request.Id);
            }
            var couponResponse = DiscountMapper.Mapper.Map<CouponDto>(coupon);
            return new BaseResponse(couponResponse);
        }
    }
}
