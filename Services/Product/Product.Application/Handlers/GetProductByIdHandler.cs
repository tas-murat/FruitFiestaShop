using MediatR;
using Product.Application.Exceptions;
using Product.Application.Mappers;
using Product.Application.Queries;
using Product.Application.Response;
using Product.Application.Responses;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }
        public async Task<BaseResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _productRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ProductNotFoundException(request.Id);
            }
            var dataResponse = ProductMapper.Mapper.Map<ProductDto>(data);
            return new BaseResponse(dataResponse);
        }
    }
}
