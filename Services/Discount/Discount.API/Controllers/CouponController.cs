using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Application.Response;
using MediatR;
using MessageBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CouponController> _logger;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;
        public CouponController(IMediator mediator, ILogger<CouponController> logger, IMessageBus messageBus, IConfiguration configuration)
        {
            _mediator = mediator;
            _logger = logger;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<BaseResponse> Get()
        {
            var query = new GetAllCouponQuery();
            var result = await _mediator.Send(query);
            return result;
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<BaseResponse> Get(int id)
        {
            var query = new GetCouponByIdQuery(id);
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpGet]
        [Route("GetByCode/{code}")]

        public async Task<BaseResponse> GetByCode(string code)
        {
            var query = new GetCouponByCodeQuery(code);
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost]
       // [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponse> Post([FromBody] CreateCouponCommand couponCommand)
        {
            var result = await _mediator.Send(couponCommand);
            await _messageBus.PublishTopicMessage(couponCommand.CouponCode, _configuration.GetValue<string>("TopicAndSubNames:CuoponTopic"), "CouponCreated");
            return result;
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponse> Put([FromBody] UpdateCouponCommand couponCommand)
        {
            var result = await _mediator.Send(couponCommand);
            return result;
        }
        [HttpDelete]
        [Route("{id:int}")]
       // [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponse> Delete(int id)
        {
            var query = new DeleteCouponByIdQuery(id);
            var result = await _mediator.Send(query);
            await _messageBus.PublishTopicMessage(id, _configuration.GetValue<string>("TopicAndSubNames:CuoponTopic"), "CouponDeleted");
            return result;
        }
    }
}
