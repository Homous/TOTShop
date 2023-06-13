using Application.Mapping;
using AutoMapper;
using FluentAssertions;
using Infrastructure.DB;
using Infrastructure.Services.ShoppingCartServices;
using Infrastructure.Test.Services.ShoppingCartTest;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Services.ShoppingCart
{
    public class TestShoppingCartServices : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        protected readonly Mapper _mapper;
        private ShoppingCartServices _services;
        public TestShoppingCartServices()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureCreated();

            var mockAutoMapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ShoppingCartProfile());
                mc.AddProfile(new ShoppingCartItemProfile());
                mc.AddProfile(new ProductProfile());
            }).CreateMapper().ConfigurationProvider;

            _mapper = new Mapper(mockAutoMapper);
            _services = new ShoppingCartServices(_context, _mapper);

        }

        [Fact]
        public async Task GetShoppingCarts_ShouldReturnShoppingCartList()
        {
            _context.ShoppingCarts.AddRange(ShoppingCartMockData.GetShoppingCart());
            _context.SaveChanges();

            /// Act
            var result = _services.GetShoppingCarts();

            /// Assert
            result.Should().HaveCount(_context.ShoppingCarts.Count());
        }

        [Fact]
        public async Task GetShoppingCartById_ShouldReturnShoppingCartObject()
        {
            _context.ShoppingCarts.AddRange(ShoppingCartMockData.GetShoppingCart());
            _context.SaveChanges();

            /// Act
            var result = _services.GetShoppingCart(1);

            /// Assert
            var shoppingCart = _context.ShoppingCarts.FirstOrDefault(x => x.Id == result.Id);
            shoppingCart.Should().NotBeNull();
        }

        [Fact]
        public void EditShoppingCart_ShouldUpdateDetailedShoppingCartDto()
        {
            _context.ShoppingCarts.AddRange(ShoppingCartMockData.GetShoppingCart());
            _context.SaveChanges();

            var editedData = ShoppingCartMockData.GetDetailedShoppingCartDto();

            /// Act
            _services.EditShoppingCart(editedData);

            /// Assert
            var shoppingCart = _context.ShoppingCarts.FirstOrDefault(x => x.Id == editedData.Id);
            shoppingCart.Should().NotBeNull();
            shoppingCart.TotalCost.Should().Be(editedData.TotalCost);
            shoppingCart.ShoppingCartItems.Count.Should().Be(editedData.ShoppingCartItems.Count);
            if (editedData.ShoppingCartItems.Count > 0)
            {
                shoppingCart.ShoppingCartItems.FirstOrDefault(x => x.Id == 1).TotalCost
                .Should().Be(editedData.ShoppingCartItems.FirstOrDefault(x => x.Id == 1).TotalCost);
            }
        }

        [Fact]
        public async Task AddShoppingCart_ShouldReturnShoppingCartAddedId()
        {
            var mockData = ShoppingCartMockData.GetShoppingCart();
            var countMockData = mockData.Count();
            _context.ShoppingCarts.AddRange(mockData);
            _context.SaveChanges();

            /// Act
            var result = _services.AddShoppingCart(ShoppingCartMockData.GetNewShoppingCartDto());

            /// Assert
            _context.ShoppingCarts.Count().Should().Be(countMockData + 1);
        }

        [Fact]
        public async Task DeleteShoppingCart_ShouldDeleteObjectFromContext()
        {
            var mockData = ShoppingCartMockData.GetShoppingCart();
            var countMockData = mockData.Count();
            _context.ShoppingCarts.AddRange(mockData);
            _context.SaveChanges();

            /// Act
            _services.DeleteShoppingCart(1);

            /// Assert
            _context.ShoppingCarts.Count().Should().Be(countMockData - 1);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
