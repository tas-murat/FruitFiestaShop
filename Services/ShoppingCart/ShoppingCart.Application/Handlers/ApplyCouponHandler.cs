using MediatR;
using ShoppingCart.Application.Commands;
using ShoppingCart.Application.Responses;
using ShoppingCart.Core.Repositories;

namespace Discount.Application.Handlers
{
    public class ApplyCouponHandler : IRequestHandler<ApplyCouponCommand, BaseResponse>
    {
        private readonly ICartHeaderRepository _cartHeaderRepository;

        public ApplyCouponHandler(ICartHeaderRepository cartHeaderRepository)
        {
            _cartHeaderRepository = cartHeaderRepository;
        }

        public async Task<BaseResponse> Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
        {
            var cartFromDb = await _cartHeaderRepository.GetFirstByUserIdAsync(request.CartHeader.UserId);
            cartFromDb.CouponCode = request.CartHeader.CouponCode;
           await _cartHeaderRepository.UpdateAsync(cartFromDb);
            

            return new BaseResponse();
        }
    }
}
