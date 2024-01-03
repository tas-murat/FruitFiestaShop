using Microsoft.EntityFrameworkCore;
using Product.Core.Common;
using Product.Core.Entities;
using Product.Infrastructure.Data.EntityConfigurations;

namespace Product.Infrastructure.Data
{
    public class ProductDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "FruitFiestaShop_ProductDb";
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        public DbSet<ProductItem> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
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
