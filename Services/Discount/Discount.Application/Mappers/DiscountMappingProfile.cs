using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.Responses;
using Discount.Core.Entities;

namespace Discount.Application.Mappers
{
    public class DiscountMappingProfile : Profile
    {
        public DiscountMappingProfile()
        {

            CreateMap<Coupon, CouponDto>().ReverseMap();
            CreateMap<Coupon, CreateCouponCommand>().ReverseMap();
            CreateMap<Coupon, UpdateCouponCommand>().ReverseMap();
        }
    }
}
