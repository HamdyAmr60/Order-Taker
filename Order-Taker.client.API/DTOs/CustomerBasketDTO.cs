using Order_Taker.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Order_Taker.client.API.DTOs
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
    }
}
