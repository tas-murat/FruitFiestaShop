using Microsoft.Data.SqlClient;
using Polly.Retry;
using Polly;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Discount.Core.Entities;

namespace Discount.Infrastructure.Data
{
    public class DiscountDbContextSeed
    {
        private readonly IConfiguration _configuration;
        private const string BASE_PATH = "../Discount.Infrastructure/Data/SeedData/";
        public DiscountDbContextSeed(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SeedAsync(DiscountDbContext context, ILogger<DiscountDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(DiscountDbContext));
            await policy.ExecuteAsync(async () => {

                using (context)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.Database.Migrate();
                        if (!context.Coupons.Any())
                        {
                            context.Database.ExecuteSqlRaw(GetIdentityExecuteSqlQuery("coupons"));

                            context.Coupons.AddRange(GetPromoMates());
                            await context.SaveChangesAsync();
                            context.Database.ExecuteSqlRaw(GetIdentityExecuteSqlQuery("coupons", false));
                        }
                        transaction.Commit();
                    }




                }
            });
        }
        private string GetIdentityExecuteSqlQuery(string tableName, bool isOn = true)
        {
            string offOn = isOn ? "ON" : "OFF";
            return $"SET IDENTITY_INSERT {DiscountDbContext.DEFAULT_SCHEMA}.{tableName} {offOn}";
        }
        private AsyncRetryPolicy CreatePolicy(ILogger<DiscountDbContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogInformation(exception, "[{prefix}] Exception {ExceptionType} with message {message}", prefix, typeof(Exception), exception.Message);
                });
        }
        private IEnumerable<Coupon> GetPromoMates()
        {
            string path = GetPath("coupons.json");

            var jsonData = System.IO.File.ReadAllText(path);
            var coupons = JsonConvert.DeserializeObject<List<Coupon>>(jsonData);
           
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
