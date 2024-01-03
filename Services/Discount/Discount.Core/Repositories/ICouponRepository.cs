using Discount.Core.Entities;

namespace Discount.Core.Repositories
{
    public interface ICouponRepository : IAsyncRepository<Coupon>
    {
        Task<Coupon> GetByCodeAsync(string code);
    }
}
