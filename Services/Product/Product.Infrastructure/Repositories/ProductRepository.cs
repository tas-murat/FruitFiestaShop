using Product.Infrastructure.Data;
using Product.Core.Entities;
using Product.Core.Repositories;
using Azure;
using Product.Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<ProductItem>, IProductRepository
    {
        public ProductRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }

       
    }
}
