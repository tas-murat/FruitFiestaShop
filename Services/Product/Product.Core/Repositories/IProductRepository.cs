using Product.Core.Entities;
using System.Linq.Expressions;

namespace Product.Core.Repositories
{
    public interface IProductRepository: IAsyncRepository<ProductItem>
    {
       
    }
}
