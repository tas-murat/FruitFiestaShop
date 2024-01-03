using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.GrpcTestService.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IMediator mediator, ILogger<DiscountService> logger )
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetCouponByCodeGrpcQuery(request.CouponCode);
            var result = await _mediator.Send(query);
            _logger.LogInformation($"Discount is retrieved for the  CouponCode: {request.CouponCode} and Amount : {result.DiscountAmount}");
            return result;
        }

       
    }
}
