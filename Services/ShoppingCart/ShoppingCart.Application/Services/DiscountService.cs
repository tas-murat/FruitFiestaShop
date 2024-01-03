using Newtonsoft.Json;
using ShoppingCart.Core.Dto;
using ShoppingCart.Core.Services;
using ShoppingCart.Application.Responses;

namespace ShoppingCart.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DiscountService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Discount");
            var response = await client.GetAsync($"/api/Coupon/GetCouponByCode/{couponCode}");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<BaseResponse>(apiContet);
            if (resp != null && resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }
            return new CouponDto();
        }
    }
}
