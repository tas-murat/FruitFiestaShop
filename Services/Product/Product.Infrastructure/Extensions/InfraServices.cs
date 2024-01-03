using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Core.Repositories;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repositories;

namespace Product.Infrastructure.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ProductDbContext>(options => {
                options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			} );
            serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            return serviceCollection;
        }
    }
}
