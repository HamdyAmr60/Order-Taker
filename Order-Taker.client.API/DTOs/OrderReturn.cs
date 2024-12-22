using Order_Taker.Core.Models.Order;

namespace Order_Taker.client.API.DTOs
{
    public class OrderReturn
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public Address ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public string DeliveryMethodTime { get; set; }
        public ICollection<OrderItemDTO> Items { get; set; } = new HashSet<OrderItemDTO>();
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
