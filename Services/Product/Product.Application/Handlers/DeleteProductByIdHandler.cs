using MediatR;
using Product.Application.Queries;
using Product.Application.Response;
using Product.Application.Utility;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductByIdHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }
        public async Task<BaseResponse> Handle(DeleteProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productItem = await _productRepository.GetByIdAsync(request.Id);
            ImageHelper.DeleteImage(productItem);
            await _productRepository.DeleteAsync(productItem);
            return new BaseResponse();
        }
    }
}
