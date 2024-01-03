using Discount.Application.Queries;
using Discount.Application.Response;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class DeleteCouponByIdHandler : IRequestHandler<DeleteCouponByIdQuery, BaseResponse>
    {
        private readonly ICouponRepository _couponRepository;

        public DeleteCouponByIdHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<BaseResponse> Handle(DeleteCouponByIdQuery request, CancellationToken cancellationToken)
        {
            await _couponRepository.DeleteAsync(new Coupon { Id = request.Id });
            return new BaseResponse();
        }
    }
}
