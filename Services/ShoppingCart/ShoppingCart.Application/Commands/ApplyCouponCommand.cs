using MediatR;
using ShoppingCart.Application.Responses;

namespace ShoppingCart.Application.Commands
{
    public class ApplyCouponCommand : CartDto, IRequest<BaseResponse>
    {

    }
}
