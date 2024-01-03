using Discount.Application.Response;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetCouponByCodeQuery : IRequest<BaseResponse>
    {
        public string Code { get; set; }

        public GetCouponByCodeQuery(string code)
        {
            Code = code;
        }
    } }
