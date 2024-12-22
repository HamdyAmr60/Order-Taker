using AutoMapper;
using Order_Taker.client.API.DTOs;
using Order_Taker.Core.Models.Order;

namespace Order_Taker.client.API.Helpers
{
    public class OrderPhotoResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderPhotoResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_configuration["appUrl"]}{source.Product.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
