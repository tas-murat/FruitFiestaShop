using Microsoft.EntityFrameworkCore;
using ShoppingCart.Core.Entities;
using ShoppingCart.Core.Repositories;
using ShoppingCart.Infrastructure.Data;

namespace ShoppingCart.Infrastructure.Repositories
{
    public class CartHeaderRepository : RepositoryBase<CartHeader>, ICartHeaderRepository
    {
        public CartHeaderRepository(ShoppingCartDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CartHeader> GetFirstByUserIdAsync(string userId)
        {
            return await _dbContext.CartHeaders.FirstAsync(f => f.UserId == userId);
        }

        public async Task<CartHeader> GetFirstOrDefaultByUserIdAsync(string userId)
        {
            return await _dbContext.CartHeaders.FirstOrDefaultAsync(f => f.UserId == userId);
        }
    }
}
