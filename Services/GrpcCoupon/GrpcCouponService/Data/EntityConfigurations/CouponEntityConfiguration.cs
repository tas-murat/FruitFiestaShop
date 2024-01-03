using Discount.Infrastructure.Data;
using GrpcCouponService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrpcCouponService.Data.EntityConfigurations
{
    public class CouponEntityConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("coupons", GrpctDbContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.Property(i => i.CouponCode)
                .HasColumnType("varchar")
                .IsRequired(true)
                .HasMaxLength(50);
            builder.Property(i => i.DiscountAmount).IsRequired(true);
        }
    }
}
