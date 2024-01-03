using MediatR;
using Product.Application.Response;

namespace Product.Application.Queries
{
    public class DeleteProductByIdQuery : IRequest<BaseResponse>
    {
        public int Id { get; set; }

        public DeleteProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
