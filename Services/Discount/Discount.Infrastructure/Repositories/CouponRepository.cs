using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Infrastructure.Repositories
{
    public class CouponRepository : RepositoryBase<Coupon>, ICouponRepository
    {
        public CouponRepository(DiscountDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Coupon> GetByCodeAsync(string code)
        {
            return await _dbContext.Coupons.FirstOrDefaultAsync(f=>f.CouponCode==code);
        }
    }
   
}
