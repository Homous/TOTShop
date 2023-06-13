using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Dtos.ShoppingCart;
using Application.Wrappers;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers;

namespace UI.Test.Controllers.Product
{
    public class TestProductController
    {
        private readonly Fixture _fixture;
        private readonly Mock<IProductServices> _moqServices;
        public TestProductController()
        {
            _fixture = new Fixture();
            _moqServices = new Mock<IProductServices>();
        }

        [Fact]
        public void Get_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var paging = new PaginationFilter(1,1);
            var productList = _fixture.CreateMany<MiniProductDto>(3).ToList();
            _moqServices.Setup(p => p.MiniDetailsProducts(paging)).Returns(productList);
            //Act
            var result = productMock.MiniDetailsProducts(paging); 

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public void Get_ShouldReturnBadRequestResult_WhenException()
        {
            //Arrange
            _moqServices.Setup(p => p.MiniDetailsProducts(null)).Throws(new Exception());
            var productMock = new ProductController(_moqServices.Object);
            //Act
            var result = productMock.MiniDetailsProducts(null);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void GetById_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            var productId = 3;
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.GetProductById(productId)).Returns(productsDto);
            var productMock = new ProductController(_moqServices.Object);

            //Act
            var result = productMock.ProudactById(productId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_ShouldReturnNotFoundResult_WhenDataNotFound()
        {
            //Arrange
            var productId = 3;
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.GetProductById(productId)).Returns(productsDto);
            var productMock = new ProductController(_moqServices.Object);
            
            //Act
            var result = productMock.ProudactById(5);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public void GetById_ShouldReturnBadRequestResult_WhenExceptions()
        {
            //Arrange
            var productId = 3;
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.GetProductById(productId)).Throws(new Exception());
            var productMock = new ProductController(_moqServices.Object);

            //Act
            var result = productMock.ProudactById(productId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnNotFoundResult_WhenDataNotFound()
        {
            //Arrange
            int productId = 2;
            var productMock = new ProductController(_moqServices.Object);
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.DeleteProduct(productId));

            //Act
            var result = productMock.DeleteProduct(3);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            int productId = 2;
            var productMock = new ProductController(_moqServices.Object);
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.DeleteProduct(productId)).Returns(true);
           
            //Act
            var result = productMock.DeleteProduct(productId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Delete_ShouldReturnBadRequestResult_WhenExceptions()
        {
            //Arrange
            int productId = 5;
            var productMock = new ProductController(_moqServices.Object);
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.DeleteProduct(productId)).Throws(new Exception());

            //Act
            var result = productMock.DeleteProduct(productId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Add_ShouldReturnOkResult_WhenDataValid()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var productDto = _fixture.Create<AddProductDto>();
            _moqServices.Setup(p => p.AddProduct(productDto));
            //Act
            var result = productMock.AddProduct(productDto);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Add_ShouldReturnBadRequestResult_WhenExceptions()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var productDto = _fixture.Create<AddProductDto>();
            _moqServices.Setup(p => p.AddProduct(productDto)).Throws(new Exception());
            //Act
            var result = productMock.AddProduct(productDto);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void FilteringData_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            string productName = "pro";
            var productMock = new ProductController(_moqServices.Object);
            var paging = new PaginationFilter(1, 1);
            var productsDto = _fixture.Create<List<MiniProductDto>>();
            _moqServices.Setup(p => p.FilteringData(productName, paging)).Returns(productsDto);

            //Act
            var result = productMock.FilteringData(productName, paging);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void FilteringData_ShouldReturnNotFoundResult_WhenDataNotFound()
        {
            //Arrange
            string productName = "aass";
            string Name = "aaa";
            var productMock = new ProductController(_moqServices.Object);
            _moqServices.Setup(p => p.FilteringData(productName,null));

            //Act
            var result = productMock.FilteringData(Name,null);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
        
        [Fact]
        public void Update_ShouldReturnOkResult_WhenDataValid()
        {
            //Arrange
            var productDto = _fixture.Create<UpdateProductDto>();
            _moqServices.Setup(p => p.UpdateProduct(productDto));
            var productMock = new ProductController(_moqServices.Object);
            //Act
            var result = productMock.UpdateProduct(productDto);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void Update_ShouldReturnBadRequestResult_WhenExceptions()
        {
            //Arrange
            var productDto = _fixture.Create<UpdateProductDto>();
            _moqServices.Setup(p => p.UpdateProduct(productDto)).Throws(new Exception());
            var productMock = new ProductController(_moqServices.Object);
            //Act
            var result = productMock.UpdateProduct(productDto);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }



    }
}
