using MediatR;
using Product.Application.Commands;
using Product.Application.Mappers;
using Product.Application.Response;
using Product.Application.Responses;
using Product.Application.Utility;
using Product.Core.Entities;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, BaseResponse>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }
        public async Task<BaseResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
          
            var productDto = ProductMapper.Mapper.Map<ProductDto>(request);
            productDto=ImageHelper.UpdateImage(productDto);

            var UpdateProduct = ProductMapper.Mapper.Map<ProductItem>(productDto);
            await _productRepository.UpdateAsync(UpdateProduct);

            return new BaseResponse(productDto);
        }
    }
}
