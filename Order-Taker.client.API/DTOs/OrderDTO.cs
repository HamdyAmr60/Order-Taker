using System.ComponentModel.DataAnnotations;

namespace Order_Taker.client.API.DTOs
{
    public class OrderDTO
    {
        [Required]
        public string BasketId { get; set; }
        [Required]

        public int DeliveryMethodId { get; set; }
        [Required]

        public AddressDTO Address { get; set; }
    }
}
