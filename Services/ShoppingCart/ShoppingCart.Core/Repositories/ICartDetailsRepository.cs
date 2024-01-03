using ShoppingCart.Core.Entities;
using System.Linq.Expressions;

namespace ShoppingCart.Core.Repositories
{
    public interface ICartDetailsRepository : IAsyncRepository<CartDetails>
    {
        Task<CartDetails> GetFirtOrDefaultCardDetail(Expression<Func<CartDetails, bool>> predicate);
        Task<CartDetails> GetFirtByCartDetailsId(int cartDetailsId);
    }
}
