using ShoppingCart.Core.Entities;

namespace ShoppingCart.Core.Repositories
{
    public interface ICartHeaderRepository : IAsyncRepository<CartHeader>
    {
        Task<CartHeader> GetFirstByUserIdAsync(string userId);
        Task<CartHeader> GetFirstOrDefaultByUserIdAsync(string userId);
    }
}
