using Microsoft.Data.SqlClient;
using Polly.Retry;
using Polly;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Product.Core.Entities;

namespace Product.Infrastructure.Data
{
    public class ProductDbContextSeed
    {
        private readonly IConfiguration _configuration;
        private const string BASE_PATH = "../Product.Infrastructure/Data/SeedData/";
        public ProductDbContextSeed(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SeedAsync(ProductDbContext context, ILogger<ProductDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ProductDbContext));
            await policy.ExecuteAsync(async () => {

                using (context)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.Database.Migrate();
                        if (!context.Products.Any())
                        {
                            context.Database.ExecuteSqlRaw(GetIdentityExecuteSqlQuery("products"));

                            context.Products.AddRange(GetProducts());
                            await context.SaveChangesAsync();
                            context.Database.ExecuteSqlRaw(GetIdentityExecuteSqlQuery("products", false));
                        }
                        transaction.Commit();
                    }




                }
            });
        }
        private string GetIdentityExecuteSqlQuery(string tableName, bool isOn = true)
        {
            string offOn = isOn ? "ON" : "OFF";
            return $"SET IDENTITY_INSERT {ProductDbContext.DEFAULT_SCHEMA}.{tableName} {offOn}";
        }
        private AsyncRetryPolicy CreatePolicy(ILogger<ProductDbContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogInformation(exception, "[{prefix}] Exception {ExceptionType} with message {message}", prefix, typeof(Exception), exception.Message);
                });
        }
        private IEnumerable<ProductItem> GetProducts()
        {
            string path = GetPath("products.json");

            var jsonData = System.IO.File.ReadAllText(path);
            var coupons = JsonConvert.DeserializeObject<List<ProductItem>>(jsonData);
           
            return coupons;
        }
        private string GetPath(string fileName)
        {
            //string path = Path.Combine("Data", "SeedData", fileName);
            string path = BASE_PATH + fileName;
            return path;
        }

    }
}
