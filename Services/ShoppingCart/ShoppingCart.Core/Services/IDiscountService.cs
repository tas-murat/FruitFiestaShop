
using ShoppingCart.Core.Dto;

namespace ShoppingCart.Core.Services
{
    public interface IDiscountService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
