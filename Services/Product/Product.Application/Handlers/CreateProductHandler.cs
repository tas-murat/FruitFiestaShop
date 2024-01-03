using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Commands;
using Product.Application.Mappers;
using Product.Application.Response;
using Product.Application.Responses;
using Product.Application.Utility;
using Product.Core.Entities;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, BaseResponse>
    {
        private readonly IProductRepository _productRepository;
        private ILogger<CreateProductHandler> _logger;
        public CreateProductHandler(IProductRepository repository, ILogger<CreateProductHandler> logger)
        {
            _productRepository = repository;
			_logger = logger;

		}
        public async Task<BaseResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<ProductItem>(request);
            if (productEntity is null)
            {
                throw new ApplicationException("There is an issue with mapping while creating new Product");
            }
            _logger.LogInformation("create çalıştı");
			var newproduct = await _productRepository.AddAsync(productEntity);

            var productResponse = ProductMapper.Mapper.Map<ProductDto>(newproduct);
            productResponse.Image=request.Image;
            productResponse.BaseUrl=request.BaseUrl;
            productResponse=ImageHelper.CreateImage(productResponse);

            productEntity = ProductMapper.Mapper.Map<ProductItem>(productResponse);

            _productRepository.ChangeTrackerClear();
			 await _productRepository.UpdateAsync(productEntity);

			_logger.LogInformation("create çalıştı");
			return new BaseResponse(productResponse);
        }
    }
}
