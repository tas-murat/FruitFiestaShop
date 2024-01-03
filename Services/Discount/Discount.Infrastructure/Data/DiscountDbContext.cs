
using Discount.Core.Common;
using Discount.Core.Entities;
using Discount.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Discount.Infrastructure.Data
{
    public class DiscountDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "FruitFiestaShop_DiscountDb";
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CouponEntityConfiguration());
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "murat";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "murat";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
