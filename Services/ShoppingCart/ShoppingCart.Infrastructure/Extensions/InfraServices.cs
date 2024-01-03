using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Core.Repositories;
using ShoppingCart.Infrastructure.Data;
using ShoppingCart.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Infrastructure.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ShoppingCartDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
            serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            serviceCollection.AddScoped<ICartDetailsRepository, CartDetailsRepository>();
            serviceCollection.AddScoped<ICartHeaderRepository, CartHeaderRepository>();
            return serviceCollection;
        }
    }
}
