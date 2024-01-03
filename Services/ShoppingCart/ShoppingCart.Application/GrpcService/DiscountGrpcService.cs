using GrpcCouponService;

namespace ShoppingCart.Application.GrpcService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetDiscount(string couponCode)
        {
            var discountRequest = new GetDiscountRequest { CouponCode = couponCode };
            return await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}
