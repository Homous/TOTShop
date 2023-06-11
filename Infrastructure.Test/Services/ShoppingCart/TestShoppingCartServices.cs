using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Services.ShoppingCart
{
    public class TestShoppingCartServices : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        public TestShoppingCartServices()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureCreated();

        }

        /* [Fact]
         public async Task GetAllAsync_ShouldReturn200Status()
         {
             /// Arrange
             var shoppingServices = new Mock<IShoppingCartServices>();
             shoppingServices.Setup(_ => _.GetShoppingCarts()).Returns(shoppingServices.get);
             var sut = new TodoController(todoService.Object);

             /// Act
             var result = (OkObjectResult)await sut.GetAllAsync();


             // /// Assert
             result.StatusCode.Should().Be(200);
         }
 */
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
