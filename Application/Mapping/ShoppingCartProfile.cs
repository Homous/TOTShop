using Application.Dtos.ShoppingCartDto;
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
            CreateMap<List<ShoppingCartDto>, List<ShoppingCartDto>>();
        }
    }
}
