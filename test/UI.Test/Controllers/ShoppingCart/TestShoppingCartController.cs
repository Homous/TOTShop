using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using UI.ActionResults;
using UI.Controllers;

namespace UI.Test.Controllers.ShoppingCart;


public class TestShoppingCartController
{
    private readonly Fixture _fixture;
    private readonly ShoppingCartController _controller;
    private readonly Mock<IShoppingCartServices> _shoppingCartServicesMock;
    public TestShoppingCartController()
    {
        _fixture = new Fixture();
        _shoppingCartServicesMock = new Mock<IShoppingCartServices>();
        _controller = new ShoppingCartController(_shoppingCartServicesMock.Object);
    }
    [Fact]
    public async void Get_WhereDataIsExists_ShouldReturn200Status()
    {
        // Arrange
        var detailedShoppingCartDtoList = _fixture.CreateMany<DetailedShoppingCartDto>(3).ToList();
        _shoppingCartServicesMock.Setup(_ => _.GetShoppingCarts()).Returns(detailedShoppingCartDtoList);


        // Act
        var result = _controller.Get();
        var obj = result as ObjectResult;
        var actual = obj.Value as ActionResultModel;

        // Assert
        Assert.Equal(200, obj.StatusCode);
        Assert.Equal(true, actual.Status);
        var actualData = actual.Data as List<DetailedShoppingCartDto>;
        Assert.Equal(3, actualData.Count);

    }


    [Fact]
    public async void GetItem_WhereObjectIsExists_ShouldReturn200Status()
    {
        // Arrange
        var detailedShoppingCartDto = _fixture.Create<DetailedShoppingCartDto>();
        _shoppingCartServicesMock.Setup(_ => _.GetShoppingCart(1)).Returns(detailedShoppingCartDto);


        // Act            
        var result = _controller.GetItemById(1);
        var obj = result as ObjectResult;
        var actual = obj.Value as ActionResultModel;

        // Assert
        Assert.Equal(200, obj.StatusCode);
        Assert.Equal(true, actual.Status);
        var actualData = actual.Data as DetailedShoppingCartDto;
        Assert.NotNull(actualData);

    }

    [Fact]
    public async Task Add_WhereObjectIsExists_ShouldReturn200Status()
    {
        // Arrange
        var shoppingCartDto = _fixture.Create<ShoppingCartDto>();
        _shoppingCartServicesMock.Setup(_ => _.AddShoppingCart(shoppingCartDto)).Returns(1);

        // Act
        var result = _controller.Add(shoppingCartDto);
        var obj = result as ObjectResult;
        var actual = obj.Value as ActionResultModel;

        // Assert
        Assert.Equal(200, obj.StatusCode);
        Assert.Equal(true, actual.Status);

    }

    [Fact]
    public async Task Delete_WhereObjectIsExists_ShouldReturn200Status()
    {
        // Arrange
        var shoppingCartDto = _fixture.Create<ShoppingCartDto>();
        _shoppingCartServicesMock.Setup(_ => _.DeleteShoppingCart(1));

        // Act
        var result = _controller.Delete(1);
        var obj = result as ObjectResult;
        var actual = obj.Value as ActionResultModel;

        // Assert
        Assert.Equal(200, obj.StatusCode);
        Assert.Equal(true, actual.Status);

    }

    [Fact]
    public async Task Update_ShouldReturn200Status()
    {
        // Arrange
        var detailedShoppingCartDto = _fixture.Create<DetailedShoppingCartDto>();
        _shoppingCartServicesMock.Setup(_ => _.EditShoppingCart(detailedShoppingCartDto));

        // Act
        var result = _controller.AddShoppingCartItemOnShoppingCart(detailedShoppingCartDto.Id, detailedShoppingCartDto);
        var obj = result as ObjectResult;
        var actual = obj.Value as ActionResultModel;

        // Assert
        Assert.Equal(200, obj.StatusCode);

    }

    [Fact]
    public async Task Update_ShouldReturnBadRequest400Status()
    {
        // Arrange
        var detailedShoppingCartDto = _fixture.Create<DetailedShoppingCartDto>();
        _shoppingCartServicesMock.Setup(_ => _.EditShoppingCart(detailedShoppingCartDto));

        // Act
        var result = _controller.AddShoppingCartItemOnShoppingCart(1, detailedShoppingCartDto);
        var obj = result as ObjectResult;
        var actual = obj.Value as ActionResultModel;

        // Assert
        Assert.Equal(400, obj.StatusCode);

    }

}
