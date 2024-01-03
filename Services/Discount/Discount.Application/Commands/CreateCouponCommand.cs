using Discount.Application.Response;
using MediatR;

namespace Discount.Application.Commands
{
    public class CreateCouponCommand : IRequest<BaseResponse>
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
