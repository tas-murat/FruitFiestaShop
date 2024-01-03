using AutoMapper;
using ShoppingCart.Application.Responses;
using ShoppingCart.Core.Entities;

namespace ShoppingCart.API.Mapper
{

    public class ShoppingCartMappingProfile : Profile
    {
        public ShoppingCartMappingProfile()
        {

            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        }
    }
}
