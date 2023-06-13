using Application.Dtos.ProductDtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        { 
            CreateMap<Product,MiniProductDto>();
            CreateMap<Product, DetailedProductDto>();
            CreateMap<DetailedProductDto, Product>();
            CreateMap<UpdateProductDto, Product > ();
            CreateMap<AddProductDto, Product>();
            //CreateMap<MiniProductDto, Product>();


        }

    }
}
