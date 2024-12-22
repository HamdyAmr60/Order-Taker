using Order_Taker.Core.Models.Order;

namespace Order_Taker.client.API.DTOs
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}