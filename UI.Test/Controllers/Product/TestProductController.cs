using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers;

namespace UI.Test.Controllers.Product
{
    public class TestProductController
    {
        private readonly Mock<IProductServices> _moqServices =new();
        private readonly List<MiniProductDto> _products;
        public TestProductController()
        {
            _products = new List<MiniProductDto>()
            {
                new MiniProductDto() {
                    ImageUrl = "Test1",
                    Price =100,
                    Name= "Test1" },
                new MiniProductDto() {
                     ImageUrl = "Test2",
                    Price =100,
                    Name= "Test2" },
                new MiniProductDto() {
                     ImageUrl = "Test3",
                    Price =100,
                    Name= "Test3" }
            };
        }

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
            Assert.IsType<BadRequestResult>(result);
            
        }

        [Fact]
        public void GetById_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            var productId = 1;
            var productMock = new ProductController(_moqServices.Object);
            var productDto = new DetailedProductDto
            {
                Id = productId,
                Name = "Test",
                Quantity = 100,
                Description = "Test",
                AddedDate = new DateTime(),
                ImageUrl = "Test",
                Price = 500
            };
            _moqServices.Setup(p => p.GetProductById(productId)).Returns(productDto);
            

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
        public void GetById_ShouldReturnBadRequestResult_WhenDataNotFound()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            int? productId = null;
            //Act
            var result = productMock.ProudactById(productId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
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
            int productId = 2;
            var productMock = new ProductController(_moqServices.Object);
            var productDto = new DetailedProductDto
            {
                Id = productId,
                Name = "Test",
                Quantity = 100,
                Description = "Test",
                AddedDate = new DateTime(),
                ImageUrl = "Test",
                Price = 500
            };
            _moqServices.Setup(p => p.DeleteProduct(productId)).Returns(true);
            //Act
            var result = productMock.DeleteProduct(productId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Add_ShouldReturnOk_WhenDataValid()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var productDto = new AddProductDto
            {
                Name = "Test",
                Quantity = 100,
                Description = "Test",
                ImageUrl = "Test",
                Price = 500
            };
            _moqServices.Setup(p => p.AddProduct(productDto));
            //Act
            var result = productMock.AddProduct(productDto);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Add_ShouldReturnBadRequest_WhenDataNotValid()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var productDto = 
                new AddProductDto
            {
                Name = "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test",
                Quantity = 100,
                Description = "Test",
                ImageUrl = "aa",
                Price = 0
            };
            _moqServices.Setup(p => p.AddProduct(productDto));

            //Act
            var result = productMock.AddProduct(productDto);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void FilteringData_ShouldReturnOkResult_WhenDataFoundByName()
        {
            //Arrange
            var productName = "pro";
            var productMock = new ProductController(_moqServices.Object);

            //Act
            var result = productMock.FilteringData(productName);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
