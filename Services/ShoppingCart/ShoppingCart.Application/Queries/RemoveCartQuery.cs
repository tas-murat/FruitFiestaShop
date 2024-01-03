using MediatR;
using ShoppingCart.Application.Responses;

namespace ShoppingCart.Application.Queries
{
    public class RemoveCartQuery : IRequest<BaseResponse>
    {
        public int CartDetailsId { get; set; }
        public RemoveCartQuery(int cartDetailsId)
        {
            CartDetailsId = cartDetailsId;
        }

    }
}
