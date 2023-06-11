using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;

namespace Infrastructure.Test.Services.ShoppingCartTest
{
    public class ShoppingCartMockData
    {
        public static List<ShoppingCartDto> Get()
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

        //public ShoppingCart ShoppingCart() { }

        /*  public static List<ShoppingCart> Get() => new List<ShoppingCart> {
                  new ShoppingCart()
                  {
                      TotalCost = 20,
                      ShoppingCartItems = new List<ShoppingCartItemDto>()
                      {
                          new ShoppingCartItem()
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
              };*/
    }
}
