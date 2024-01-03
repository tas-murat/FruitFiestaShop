using Discount.Application.Response;
using MediatR;

namespace Discount.Application.Queries
{
    public class DeleteCouponByIdQuery : IRequest<BaseResponse>
    {
        public int Id { get; set; }

        public DeleteCouponByIdQuery(int id)
        {
            Id = id;
        }
    }
}
