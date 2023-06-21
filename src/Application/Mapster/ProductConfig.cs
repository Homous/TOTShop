using Application.Dtos.ProductDtos;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapster;

public class ProductConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, MiniProductDto>();
        config.NewConfig<Product, DetailedProductDto>();
        config.NewConfig<DetailedProductDto, Product>();
        config.NewConfig<UpdateProductDto, Product>();
        config.NewConfig<AddProductDto, Product>();
    }
}
