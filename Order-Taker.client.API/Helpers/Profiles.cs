using AutoMapper;
using Order_Taker.client.API.DTOs;
using Order_Taker.Core.Models;
using Order_Taker.Core.Models.Identity;
using Order_Taker.Core.Models.Order;

namespace Order_Taker.client.API.Helpers
{
    public class Profiles :Profile
    {
        public Profiles()
        {
            CreateMap<Product, ProductDTO>().ForMember(D => D.BrandName, Options => Options.MapFrom(S => S.ProductBrand.Name))
           .ForMember(D => D.BrandId, Options => Options.MapFrom(S => S.ProductBrandId))
            .ForMember(D => D.TypeName, Options => Options.MapFrom(S => S.ProductType.Name))
            .ForMember(D => D.TypeId, Options => Options.MapFrom(S => S.ProductTypeId))
            .ForMember(D=>D.PictureUrl , Options=>Options.MapFrom<ProductPhotoResolver>());
            CreateMap<Core.Models.Identity.Address, AddressDTO>().ReverseMap();
            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();
            CreateMap<AddressDTO, Core.Models.Order.Address>();
        }
    }
}
