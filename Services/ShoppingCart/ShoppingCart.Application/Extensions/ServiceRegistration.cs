using GrpcCouponService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.GrpcService;
using ShoppingCart.Application.Services;
using ShoppingCart.Application.Utility;
using ShoppingCart.Core.Services;
using System.Reflection;

namespace ShoppingCart.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddHttpContextAccessor();
            services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddHttpClient("Product", u => u.BaseAddress =new Uri(configuration["ServiceUrls:ProductAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
            services.AddHttpClient("Discount", u => u.BaseAddress =new Uri(configuration["ServiceUrls:DiscountAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

            services.AddScoped<DiscountGrpcService>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
                (o => o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));
            return services;
        }
    }
}
