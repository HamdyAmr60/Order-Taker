using Order_Taker.Core.Models;

namespace Order_Taker.client.API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }
        public int TypeId { get; set; }

        public string TypeName { get; set; }
    }
}
