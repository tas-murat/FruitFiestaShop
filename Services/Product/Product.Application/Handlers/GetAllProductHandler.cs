using MediatR;
using Product.Application.Mappers;
using Product.Application.Queries;
using Product.Application.Response;
using Product.Application.Responses;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<BaseResponse> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            var productResponses = ProductMapper.Mapper.Map<IList<ProductDto>>(products);
            return new BaseResponse(productResponses);
        }
    }
}
