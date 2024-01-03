using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Commands;
using Product.Application.Queries;
using Product.Application.Response;

namespace Product.API.Controllers
{
    public class ProductController: ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        public async Task<BaseResponse> GetAsync()
        {
            var query = new GetAllProductQuery();
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<BaseResponse> GetAsync(int id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost]
       [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponse> PostAsync([FromForm] CreateProductCommand productCommand)
        {
            productCommand.BaseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}"; ;
            var result = await _mediator.Send(productCommand);
            return result;
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponse> PutAsync([FromForm] UpdateProductCommand updateCommand)
        {
            updateCommand.BaseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}"; ;
            var result = await _mediator.Send(updateCommand);
            return result;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var query = new DeleteProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return result;
        }
        //public string GetBaseUrl()
        //{
        //    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
        //    return baseUrl ;
        //}
    }
}
