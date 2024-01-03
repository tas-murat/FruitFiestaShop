using Discount.Infrastructure.Data;
using Grpc.Core;
using GrpcCouponService;
using GrpcCouponService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GrpcCouponService.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        
        private readonly ILogger<DiscountService> _logger;
        private readonly GrpctDbContext _db;

        public DiscountService(ILogger<DiscountService> logger, GrpctDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            
            var coupon = await _db.Coupons.FirstOrDefaultAsync(f => f.CouponCode == request.CouponCode);

            if (coupon == null)
            {
                throw new CouponNotFoundException(request.CouponCode);
            }
            var couponResponse = new CouponModel
            {
                Id = coupon.Id,
                CouponCode = coupon.CouponCode,
                DiscountAmount = coupon.DiscountAmount,
                MinAmount = coupon.MinAmount
            };
            _logger.LogInformation($"Discount is retrieved for the  CouponCode: {couponResponse.CouponCode} and Amount : {couponResponse.DiscountAmount}");

            return couponResponse;
        }
    }
}
