
using ShoppingCart.Core.Dto;

namespace ShoppingCart.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
