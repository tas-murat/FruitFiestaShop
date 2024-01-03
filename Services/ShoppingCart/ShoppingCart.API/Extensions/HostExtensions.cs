using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace ShoppingCart.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    logger.LogInformation("Migration işlemi başlatıldı. Kullanılan context : {DbContextName}", typeof(TContext).Name);
                    var retry = Policy.Handle<SqlException>().WaitAndRetry(new TimeSpan[]
                    {
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(8),
                    });
                    retry.Execute(() => InvokeSeeder( context, services));
                    logger.LogInformation("Migration işlemi bitti context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Migration Error: [{prefix}] Exception {ExceptionType} with message {message}", typeof(TContext).Name, typeof(Exception), exception.Message);
                }
                return host;
            }
        }

        private static void InvokeSeeder<TContext>(TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }

}
