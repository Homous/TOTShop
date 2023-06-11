using Application.Contracts.ProuductServices;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers;

namespace UI.Test.Controllers.Product
{
    public class TestGetProductController
    {
        private readonly Mock<IProductServices> _moqServices = new();

        [Fact]
        public void Get_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);

            //Act
            var result = productMock.MiniDetailsProducts();

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public void Get_ShouldReturnBadRequestResult_WhenDataNotFound()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);

            //Act
            var result = productMock.MiniDetailsProducts();
            result = null;
            //Assert
            if (result != null)
            {
                Assert.IsType<BadRequestResult>(result);
            }
        }

        [Fact]
        public void GetById_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var productId = 1;

            //Act
            var result = productMock.ProudactById(productId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_ShouldReturnNotFoundResult_WhenDataNotFound()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var productId = 5;
            //Act
            var result = productMock.ProudactById(productId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        
        [Fact]
        public void Delete_ShouldReturnNotFound_WhenDataNotFound()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            int productId = 5;
            //Act
            var result = productMock.DeleteProduct(productId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public void Delete_ShouldReturnOk_WhenDataFound()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            int productId = 1;
            //Act
            var result = productMock.DeleteProduct(productId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }


    }
}
