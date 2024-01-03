using MediatR;
using ShoppingCart.Application.Queries;
using ShoppingCart.Application.Responses;
using ShoppingCart.Core.Repositories;

namespace ShoppingCart.Application.Handlers
{
    public class RemoveCartHandler : IRequestHandler<RemoveCartQuery, BaseResponse>
    {
        private readonly ICartHeaderRepository _cartHeaderRepository;
        private readonly ICartDetailsRepository _cartDetailsRepository;

        public RemoveCartHandler(ICartHeaderRepository cartHeaderRepository, ICartDetailsRepository cartDetailsRepository)
        {
            _cartHeaderRepository = cartHeaderRepository;
            _cartDetailsRepository = cartDetailsRepository;
        }
        public async Task<BaseResponse> Handle(RemoveCartQuery request, CancellationToken cancellationToken)
        {
            var cartDetails =await _cartDetailsRepository.GetFirtByCartDetailsId(request.CartDetailsId);
            int totalCountofCartItem = _cartDetailsRepository.GetAllAsync(u => u.CartHeaderId == cartDetails.CartHeaderId).Result.Count();
           await  _cartDetailsRepository.DeleteAsync(cartDetails);
            if (totalCountofCartItem == 1)
            {
                var cartHeaderToRemove = await _cartHeaderRepository.GetByIdAsync(g => g.CartHeaderId == cartDetails.CartHeaderId);

              await  _cartHeaderRepository.DeleteAsync(cartHeaderToRemove);
            }

            return new BaseResponse();
        }
    }

  
}
