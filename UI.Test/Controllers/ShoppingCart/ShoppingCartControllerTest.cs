using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using UI.ActionResults;
using UI.Controllers;

namespace UI.Test.Controllers.ShoppingCart
{

    public class ShoppingCartControllerTest
    {
        private Fixture _fixture;
        private ShoppingCartController _controller;
        private Mock<IShoppingCartServices> _shoppingCartServicesMock;
        public ShoppingCartControllerTest()
        {
            _fixture = new Fixture();
            _shoppingCartServicesMock = new Mock<IShoppingCartServices>();
        }
        [Fact]
        public async void Get_ShouldReturn200Status()
        {
            // Arrange
            var detailedShoppingCartDtoList = _fixture.CreateMany<DetailedShoppingCartDto>(3).ToList();
            _shoppingCartServicesMock.Setup(_ => _.GetShoppingCarts()).Returns(detailedShoppingCartDtoList);
            _controller = new ShoppingCartController(_shoppingCartServicesMock.Object);

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
        public async void GetItem_ShouldReturn200Status()
        {
            // Arrange
            var detailedShoppingCartDto = _fixture.Create<DetailedShoppingCartDto>();
            _shoppingCartServicesMock.Setup(_ => _.GetShoppingCart(1)).Returns(detailedShoppingCartDto);
            _controller = new ShoppingCartController(_shoppingCartServicesMock.Object);


            // Act            
            var result = _controller.GetItem(1);
            var obj = result as ObjectResult;
            var actual = obj.Value as ActionResultModel;

            // Assert
            Assert.Equal(200, obj.StatusCode);
            Assert.Equal(true, actual.Status);
            var actualData = actual.Data as DetailedShoppingCartDto;
            Assert.NotNull(actualData);

        }

        [Fact]
        public async Task Add_ShouldReturn200Status()
        {
            // Arrange
            var shoppingCartDto = _fixture.Create<ShoppingCartDto>();
            _shoppingCartServicesMock.Setup(_ => _.AddShoppingCart(shoppingCartDto)).Returns(1);
            _controller = new ShoppingCartController(_shoppingCartServicesMock.Object);

            // Act
            var result = _controller.Add(shoppingCartDto);
            var obj = result as ObjectResult;
            var actual = obj.Value as ActionResultModel;

            // Assert
            Assert.Equal(200, obj.StatusCode);
            Assert.Equal(true, actual.Status);

        }

        [Fact]
        public async Task Delete_ShouldReturn200Status()
        {
            // Arrange
            var shoppingCartDto = _fixture.Create<ShoppingCartDto>();
            _shoppingCartServicesMock.Setup(_ => _.DeleteShoppingCart(1));
            _controller = new ShoppingCartController(_shoppingCartServicesMock.Object);

            // Act
            var result = _controller.Delete(1);
            var obj = result as ObjectResult;
            var actual = obj.Value as ActionResultModel;

            // Assert
            Assert.Equal(200, obj.StatusCode);
            Assert.Equal(true, actual.Status);

        }


    }
}
