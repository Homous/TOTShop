using Application.Dtos.ProductDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, MiniProductDto>();
            CreateMap<Product, DetailedProductDto>().ReverseMap();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<AddProductDto, Product>();


        }

    }
}
