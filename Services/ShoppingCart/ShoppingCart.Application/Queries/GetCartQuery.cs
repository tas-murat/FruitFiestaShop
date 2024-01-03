using MediatR;
using ShoppingCart.Application.Responses;
namespace ShoppingCart.Application.Queries
{
    public class GetCartQuery : IRequest<BaseResponse>
    {
        public string UserId { get; set; }
        public GetCartQuery(string userId)
        {
            UserId = userId;
        }

    }
}
