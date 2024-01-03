using MediatR;
using ShoppingCart.Application.Responses;


namespace ShoppingCart.Application.Commands
{
    public class CartUpsertCommand : CartDto, IRequest<BaseResponse>
    {

    }
}
