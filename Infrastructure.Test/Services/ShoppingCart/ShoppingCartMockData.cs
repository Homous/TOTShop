using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DB;
using Infrastructure.Services.ShoppingCartServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Services.ShoppingCartTest
{
    public class ShoppingCartMockData
    {
        public static ApplicationDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new ApplicationDbContext(options);
            return context;
        }
        public static ShoppingCartServices GetShoppingCartService(ApplicationDbContext context)
        {
            var mockAutoMapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ShoppingCartProfile());
                mc.AddProfile(new ShoppingCartItemProfile());
                mc.AddProfile(new ProductProfile());
            }).CreateMapper().ConfigurationProvider;

            Mapper mapper = new Mapper(mockAutoMapper);
            return new ShoppingCartServices(context, mapper);
        }

        public static List<ShoppingCartDto> GetShoppingCartDtos()
        {
            return new List<ShoppingCartDto> {
                new ShoppingCartDto()
                {
                    TotalCost = 20,
                    ShoppingCartItems = new List<ShoppingCartItemDto>()
                    {
                        new ShoppingCartItemDto()
                        {
                            TotalCost = 5,
                            Count = 1,
                            ProductId = 1,
                            ShoppingCartId =1
                        },
                        new ShoppingCartItemDto()
                        {
                            TotalCost = 5,
                            Count = 1,
                            ProductId = 2,
                            ShoppingCartId =1
                        },
                        new ShoppingCartItemDto()
                        {
                            TotalCost = 5,
                            Count = 1,
                            ProductId = 3,
                            ShoppingCartId =1
                        },
                        new ShoppingCartItemDto()
                        {
                            TotalCost = 5,
                            Count = 1,
                            ProductId = 4,
                            ShoppingCartId =1
                        }
                    }
                }
            };

        }
        public static List<Domain.Entities.ShoppingCart> GetShoppingCart()
        {
            return new List<Domain.Entities.ShoppingCart> {
                new Domain.Entities.ShoppingCart()
                {
                    Id = 1,
                    TotalCost = 20,
                    AddedDate = DateTime.Now,
                    ShoppingCartItems = new List<ShoppingCartItem>()
                    {
                        new ShoppingCartItem()
                        {
                            Id=1,
                            AddedDate= DateTime.Now,
                            TotalCost = 5,
                            Count = 1,
                            ShoppingCartId =1,
                            ProductId = 1,
                            Product = new Domain.Entities.Product()
                            {
                                Id=1,
                                AddedDate= DateTime.Now,
                                Description = "Descriptions for product 1",
                                Name = "Product 1",
                                ImageUrl = "Image Url for product 1",
                                Price = 1,
                                Quantity = 1,

                            }
                        },
                        new ShoppingCartItem()
                        {
                            Id=2,
                            AddedDate= DateTime.Now,
                            TotalCost = 5,
                            Count = 1,
                            ShoppingCartId =1,
                            ProductId = 2,
                            Product = new Domain.Entities.Product()
                            {
                                Id=2,
                                AddedDate= DateTime.Now,
                                Description = "Descriptions for product 2",
                                Name = "Product 2",
                                ImageUrl = "Image Url for product 2",
                                Price = 2,
                                Quantity = 2,

                            }
                        }
                    }
                }
            };

        }

        public static DetailedShoppingCartDto GetDetailedShoppingCartDto()
        {
            return new DetailedShoppingCartDto()
            {
                Id = 1,
                TotalCost = 5,
                ShoppingCartItems = new List<DetailedShoppingCartItemDto>
                {
                    new DetailedShoppingCartItemDto()
                        {
                            Id=1,
                            TotalCost = 50,
                            Count = 1,
                            ProductId = 1
                        }
                }
            };
        }

        public static ShoppingCartDto GetNewShoppingCartDto()
        {
            return new ShoppingCartDto()
            {
                TotalCost = 20,
                ShoppingCartItems = new List<ShoppingCartItemDto>()
                    {
                        new ShoppingCartItemDto()
                        {
                            TotalCost = 5,
                            Count = 1,
                            ProductId = 1
                        },
                        new ShoppingCartItemDto()
                        {
                            TotalCost = 5,
                            Count = 1,
                            ProductId = 2
                        },
                        new ShoppingCartItemDto()
                        {
                            TotalCost = 5,
                            Count = 1,
                            ProductId = 3
                        },
                        new ShoppingCartItemDto()
                        {
                            TotalCost = 5,
                            Count = 1,
                            ProductId = 4
                        }
                    }
            };
        }

    }
}
