using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Core.Entities;

namespace Product.Infrastructure.Data.EntityConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.ToTable("products", ProductDbContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.Property(i => i.Name)
                .HasColumnType("varchar")
                .IsRequired(true)
                .HasMaxLength(250);
            builder.Property(i => i.CategoryName)
                .HasColumnType("varchar")
                .HasMaxLength(250);

            builder.Property(i => i.ImageUrl)
                .HasColumnType("varchar")
                .HasMaxLength(250);

            builder.Property(i => i.ImageLocalPath)
                .HasColumnType("varchar")
                .HasMaxLength(250);

            builder.Property(i => i.Description)
                .HasColumnType("varchar")
                .HasMaxLength(550);

            builder.Property(i => i.Price).IsRequired(true);
        }
    }
}
