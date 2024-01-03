using MediatR;
using Product.Application.Response;

namespace Product.Application.Queries
{
    public class GetProductByIdQuery : IRequest<BaseResponse>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    } 
}
