using Application.Dtos.ShoppingCartItemDto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class ShoppingCartItemProfile : Profile
    {
        public ShoppingCartItemProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>();
            CreateMap<ShoppingCartItem, DetailedShoppingCartItemDto>();
            CreateMap<ShoppingCartItemDto, ShoppingCartItemDto>();

        }
    }
}
