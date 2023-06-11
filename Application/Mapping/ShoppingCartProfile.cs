using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>();
            CreateMap<ShoppingCart, DetailedShoppingCartDto>();

            CreateMap<ShoppingCartDto, ShoppingCart>()
                        .ForMember(dest => dest.ShoppingCartItems,
                        opt => opt.MapFrom(src => src.ShoppingCartItems)).ReverseMap();
            CreateMap<DetailedShoppingCartDto, ShoppingCart>()
                        .ForMember(dest => dest.ShoppingCartItems,
                        opt => opt.MapFrom(src => src.ShoppingCartItems))
                        .ReverseMap();

            CreateMap<EditShoppingCartItemDto, ShoppingCart>().ReverseMap();
        }
    }
}
