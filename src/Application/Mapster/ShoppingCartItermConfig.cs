using Application.Dtos.ShoppingCartItem;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapster;

public class ShoppingCartItermConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShoppingCartItem, ShoppingCartItemDto>();
        config.NewConfig<ShoppingCartItem, DetailedShoppingCartItemDto>();
        config.NewConfig<ShoppingCartItem, UpdateShoppingCartItemDto>();
    }
}
