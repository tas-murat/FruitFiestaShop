using AutoMapper;
using Product.Application.Commands;
using Product.Application.Responses;
using Product.Core.Entities;

namespace Product.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {

            CreateMap<ProductItem, ProductDto>().ReverseMap();
            CreateMap<ProductItem, CreateProductCommand>().ReverseMap();
            CreateMap<ProductItem, UpdateProductCommand>().ReverseMap();
            CreateMap<ProductDto, UpdateProductCommand>().ReverseMap();
            CreateMap<ProductDto, CreateProductCommand>().ReverseMap();
        }
    }
}
