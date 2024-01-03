using MediatR;
using MessageBus;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Commands;
using ShoppingCart.Application.Queries;
using ShoppingCart.Application.Responses;

namespace ShoppingCart.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CartAPIController> _logger;
        private readonly IMessageBus _messageBus;
        private IConfiguration _configuration;

        public CartAPIController(IMediator mediator, ILogger<CartAPIController> logger, IMessageBus messageBus, IConfiguration configuration)
        {
            _mediator = mediator;
            _logger = logger;
            _messageBus = messageBus;
            _configuration = configuration;
        }
        [HttpGet("GetCart/{userId}")]
        public async Task<BaseResponse> GetCart(string userId)
        {
            var query = new GetCartQuery(userId);
            var result = await _mediator.Send(query);
            return result;
        }


        [HttpPost("ApplyCoupon")]
        public async Task<BaseResponse> ApplyCoupon([FromBody] ApplyCouponCommand cartDto)
        {
            var result = await _mediator.Send(cartDto);
            return result;
        }

        [HttpPost("EmailCartRequest")]
        public async Task<BaseResponse> EmailCartRequest([FromBody] CartDto cartDto)
        {
            BaseResponse response=new BaseResponse();
            try
            {
                await _messageBus.PublishMessage(cartDto, _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue"));
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }
        [HttpPost("CartUpsert")]
        public async Task<BaseResponse> CartUpsert(CartUpsertCommand cartDto)
        {
            var result = await _mediator.Send(cartDto);
            return result;
        }



        [HttpPost("RemoveCart")]
        public async Task<BaseResponse> RemoveCart([FromBody] int cartDetailsId)
        {
            var query = new RemoveCartQuery(cartDetailsId);
            var result = await _mediator.Send(query);
            return result;
        }

    }
}
