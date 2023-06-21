using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapster;

public class ShoppingCartConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShoppingCart, ShoppingCartDto>();
        config.NewConfig<ShoppingCart, DetailedShoppingCartDto>();

        config.NewConfig<ShoppingCartDto, ShoppingCart>()
                    .Map(dest => dest.ShoppingCartItems,
                    src => src.ShoppingCartItems);
        config.NewConfig<DetailedShoppingCartDto, ShoppingCart>()
                    .Map(dest => dest.ShoppingCartItems,
                   src => src.ShoppingCartItems);

        config.NewConfig<EditShoppingCartItemDto, ShoppingCart>();
    }
}
