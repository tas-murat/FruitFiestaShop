using Microsoft.AspNetCore.Http;

namespace Product.Application.Responses
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public string BaseUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
