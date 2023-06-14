using Application.Dtos.ShoppingCartItem;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class ShoppingCartItemProfile : Profile
{
    public ShoppingCartItemProfile()
    {
        CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
        CreateMap<ShoppingCartItem, DetailedShoppingCartItemDto>().ReverseMap();
        CreateMap<ShoppingCartItem, UpdateShoppingCartItemDto>().ReverseMap();
    }
}
