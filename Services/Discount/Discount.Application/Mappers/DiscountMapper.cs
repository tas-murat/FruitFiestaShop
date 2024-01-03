using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Mappers
{
    public static class DiscountMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<DiscountMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        //Lazy imapper ihtiyaç duyulduğunda sadece bir tane üretir ve her seferinde onu kullanır.sadece bir kere oluşturur
        public static IMapper Mapper => Lazy.Value;
    }
}
