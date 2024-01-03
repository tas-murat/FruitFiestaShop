using MediatR;
using ShoppingCart.API.Mapper;
using ShoppingCart.Application.Commands;
using ShoppingCart.Application.Responses;
using ShoppingCart.Core.Entities;
using ShoppingCart.Core.Repositories;

namespace Discount.Application.Handlers
{
    public class CartUpsertHandler : IRequestHandler<CartUpsertCommand, BaseResponse>
    {
        private readonly ICartHeaderRepository _cartHeaderRepository;
        private readonly ICartDetailsRepository _cartDetailsRepository;

        public CartUpsertHandler(ICartHeaderRepository cartHeaderRepository, ICartDetailsRepository cartDetailsRepository)
        {
            _cartHeaderRepository = cartHeaderRepository;
            _cartDetailsRepository = cartDetailsRepository;
        }

        public async Task<BaseResponse> Handle(CartUpsertCommand request, CancellationToken cancellationToken)
        {
            var cartHeaderFromDb = await _cartHeaderRepository.GetFirstOrDefaultByUserIdAsync(request.CartHeader.UserId);

            if (cartHeaderFromDb == null)
            {
                
                CartHeader cartHeader = ShoppingCartMapper.Mapper.Map<CartHeader>(request.CartHeader);
                await _cartHeaderRepository.AddAsync(cartHeader);


                request.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                var cartDetails = ShoppingCartMapper.Mapper.Map<CartDetails>(request.CartDetails.First());
                await _cartDetailsRepository.AddAsync(cartDetails);

            }
            else
            {
               
                var cartDetailsFromDb = await _cartDetailsRepository.GetFirtOrDefaultCardDetail(
                    u => u.ProductId == request.CartDetails.First().ProductId &&
                    u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                if (cartDetailsFromDb == null)
                {
                   
                    request.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    var cartDetails = ShoppingCartMapper.Mapper.Map<CartDetails>(request.CartDetails.First());
                    await _cartDetailsRepository.AddAsync(cartDetails);
                }
                else
                {
                   
                    request.CartDetails.First().Count += cartDetailsFromDb.Count;
                    request.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                    request.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                    var cartDetails = ShoppingCartMapper.Mapper.Map<CartDetails>(request.CartDetails.First());
                    await _cartDetailsRepository.UpdateAsync(cartDetails);
                }
            }

            return new BaseResponse(request);
        }
    }
}
