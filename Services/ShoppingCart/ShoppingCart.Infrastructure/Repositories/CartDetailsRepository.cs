using Microsoft.EntityFrameworkCore;
using ShoppingCart.Core.Entities;
using ShoppingCart.Core.Repositories;
using ShoppingCart.Infrastructure.Data;
using System.Linq.Expressions;

namespace ShoppingCart.Infrastructure.Repositories
{
    public class CartDetailsRepository : RepositoryBase<CartDetails>, ICartDetailsRepository
    {
        public CartDetailsRepository(ShoppingCartDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CartDetails> GetFirtByCartDetailsId(int cartDetailsId)
        {
            return await _dbContext.CartDetails.FirstAsync(f=>f.CartDetailsId==cartDetailsId);
        }

        public async Task<CartDetails> GetFirtOrDefaultCardDetail(Expression<Func<CartDetails, bool>> predicate)
        {
            return await _dbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(predicate);
        }
    }
}
