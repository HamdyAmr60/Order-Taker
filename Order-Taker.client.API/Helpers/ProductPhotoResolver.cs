using AutoMapper;
using Microsoft.Extensions.Configuration;
using Order_Taker.client.API.DTOs;
using Order_Taker.Core.Models;

namespace Order_Taker.client.API.Helpers
{
    public class ProductPhotoResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPhotoResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["appUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
