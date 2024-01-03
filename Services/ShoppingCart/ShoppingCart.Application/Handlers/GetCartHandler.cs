using MediatR;
using ShoppingCart.API.Mapper;
using ShoppingCart.Application.GrpcService;
using ShoppingCart.Application.Queries;
using ShoppingCart.Application.Responses;
using ShoppingCart.Core.Dto;
using ShoppingCart.Core.Repositories;
using ShoppingCart.Core.Services;

namespace ShoppingCart.Application.Handlers
{
    public class GetCartHandler : IRequestHandler<GetCartQuery, BaseResponse>
    {
        private readonly ICartHeaderRepository _cartHeaderRepository;
        private readonly ICartDetailsRepository _cartDetailsRepository;
        private readonly IProductService _productService;
        // private readonly IDiscountService _discountService;
        private readonly DiscountGrpcService _discountGrpcService;
        public GetCartHandler(ICartHeaderRepository cartHeaderRepository, ICartDetailsRepository cartDetailsRepository, IProductService productService, DiscountGrpcService discountGrpcService)
        {
            _cartHeaderRepository = cartHeaderRepository;
            _cartDetailsRepository = cartDetailsRepository;
            _productService = productService;
            _discountGrpcService = discountGrpcService;
        }
        public async Task<BaseResponse> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cardHeader = await _cartHeaderRepository.GetFirstByUserIdAsync(request.UserId);
            CartDto cart = new()
            {
                CartHeader = ShoppingCartMapper.Mapper.Map< CartHeaderDto>(cardHeader)
            };
            var cartDetails = await _cartDetailsRepository.GetAllAsync(u => u.CartHeaderId == cart.CartHeader.CartHeaderId);
            cart.CartDetails = ShoppingCartMapper.Mapper.Map<IEnumerable<CartDetailsDto>>(cartDetails);

            IEnumerable<ProductDto> productDtos = await _productService.GetProducts();
            foreach (var item in cart.CartDetails)
            {
                item.Product = productDtos.FirstOrDefault(u => u.Id == item.ProductId);
                cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
            }

            
            if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
            {
                //CouponDto coupon = await _discountService.GetCoupon(cart.CartHeader.CouponCode);
                var coupon = await _discountGrpcService.GetDiscount(cart.CartHeader.CouponCode);
                if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
                {
                    cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                    cart.CartHeader.Discount = coupon.DiscountAmount;
                }
            }

            return new BaseResponse(cart);
        }
    }  

  
}
