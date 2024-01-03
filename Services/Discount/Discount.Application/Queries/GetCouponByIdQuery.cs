using Discount.Application.Response;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetCouponByIdQuery : IRequest<BaseResponse>
    {
        public int Id { get; set; }

        public GetCouponByIdQuery(int id)
        {
            Id = id;
        }
    } 
}
