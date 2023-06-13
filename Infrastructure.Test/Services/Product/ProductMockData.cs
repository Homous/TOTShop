using Application.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Test.Services.Product
{
    public class ProductMockData
    {
        public static List<DetailedProductDto> GetProducts(){
            return new List<DetailedProductDto>
            {
                new DetailedProductDto
                {Id=1,Name = "Pro1", Price = 107, ImageUrl = "Test", Description = "Test", Quantity = 10},
                new DetailedProductDto
               {Id=2,Name = "Pro2", Price = 157, ImageUrl = "Test", Description = "Test", Quantity = 10},
                new DetailedProductDto
                {Id=3,Name = "Pro3", Price = 10, ImageUrl = "Test", Description = "Test", Quantity = 10},
                new DetailedProductDto
                {Id=4,Name = "Pro4", Price = 17, ImageUrl = "Test", Description = "Test", Quantity = 10}
            };
            }
        public static List<MiniProductDto> GetEmptyProducts()
        {
            return new List<MiniProductDto>();
        }
        public static AddProductDto AddProduct()
        {
            return new AddProductDto()
            {
                ImageUrl ="Test",
                Name = "Pro Test",
                Price = 100,
                Description = "Test Products",
                Quantity =200
            };
        }
        public static UpdateProductDto UpdateProduct()
        {
            return new UpdateProductDto()
            {
                Id=2,
                ImageUrl = "Test",
                Name = "Pro Test",
                Price = 100,
                Description = "Test Products",
                Quantity = 200
            };
        }
        public static DetailedProductDto GetProductById()
        {
            return new DetailedProductDto()
            {
                Id = 5,
                AddedDate = new DateTime(),
                ImageUrl = "Test new",
                Name = " Test new",
                Price = 100,
                Description = "Test Products new",
                Quantity = 200
            };
        }
    }
}
