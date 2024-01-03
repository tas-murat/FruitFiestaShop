namespace GrpcCouponService.Models
{

    public class Coupon: EntityBase
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
