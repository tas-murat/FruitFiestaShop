using Discount.Application.Response;
using MediatR;

namespace Discount.Application.Commands
{
    public class UpdateCouponCommand : IRequest<BaseResponse>
    {
        
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
