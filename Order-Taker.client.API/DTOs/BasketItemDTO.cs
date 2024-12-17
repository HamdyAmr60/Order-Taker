using System.ComponentModel.DataAnnotations;

namespace Order_Taker.client.API.DTOs
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(.01,double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
    }
}