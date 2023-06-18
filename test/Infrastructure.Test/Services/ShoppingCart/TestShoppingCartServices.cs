using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;
using Application.Mapping;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Infrastructure.DB;
using Infrastructure.Services.ShoppingCartServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Test.Services.ShoppingCart;

public class TestShoppingCartServices : IDisposable
{
    private readonly Fixture _fixture;
    private readonly ApplicationDbContext _context;
    private readonly Mapper _mapper;
    private readonly IShoppingCartServices _services;

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
        _fixture = new Fixture();
        _mapper = new Mapper(mockAutoMapper);
        _services = new ShoppingCartServices(_context, _mapper);

    }

    [Fact]
    public async Task GetShoppingCarts_WhereDataIsExists__ShouldReturnShoppingCartList()
    {
        var detailedShoppingCartDto = _fixture.CreateMany<DetailedShoppingCartDto>(3).ToList();
        var mapData = _mapper.Map<List<Domain.Entities.ShoppingCart>>(detailedShoppingCartDto);
        _context.ShoppingCarts.AddRange(mapData);
        _context.SaveChanges();

        /// Act
        var result = _services.GetShoppingCarts();

        /// Assert
        result.Should().HaveCount(_context.ShoppingCarts.Count());
    }

    [Fact]
    public async Task GetShoppingCartById_WhereObjectIsExists__ShouldReturnShoppingCartObject()
    {
        var detailedShoppingCartDto = _fixture.Create<DetailedShoppingCartDto>();
        var mapData = _mapper.Map<Domain.Entities.ShoppingCart>(detailedShoppingCartDto);
        _context.ShoppingCarts.AddRange(mapData);
        _context.SaveChanges();

        /// Act
        var result = _services.GetShoppingCart(mapData.Id);

        /// Assert
        var shoppingCart = _context.ShoppingCarts.FirstOrDefault(x => x.Id == result.Id);
        shoppingCart.Should().NotBeNull();
    }

    [Fact]
    public void EditShoppingCart_WhereObjectIsExists__ShouldUpdateDetailedShoppingCartDto()
    {
        var editShoppingCartDto = _fixture.Create<DetailedShoppingCartDto>();
        var mapData = _mapper.Map<Domain.Entities.ShoppingCart>(editShoppingCartDto);
       _context.ShoppingCarts.AddRange(mapData);
        _context.SaveChanges();


        /// Act
        editShoppingCartDto.TotalCost = 20;
        _services.EditShoppingCart(editShoppingCartDto);

        /// Assert
        var shoppingCart = _context.ShoppingCarts.FirstOrDefault(x => x.Id == editShoppingCartDto.Id);
        shoppingCart.Should().NotBeNull();
        shoppingCart.TotalCost.Should().Be(editShoppingCartDto.TotalCost);
        var item = editShoppingCartDto.ShoppingCartItems.First();
        shoppingCart.ShoppingCartItems.Count.Should().Be(editShoppingCartDto.ShoppingCartItems.Count);
        if (editShoppingCartDto.ShoppingCartItems.Count > 0)
        {
            
            shoppingCart.ShoppingCartItems.FirstOrDefault(x => x.Id == item.Id).TotalCost
            .Should().Be(editShoppingCartDto.ShoppingCartItems.FirstOrDefault(x => x.Id == item.Id).TotalCost);
        }
    }

    [Fact]
    public async Task AddShoppingCart_WhereObjectIsExists__ShouldReturnShoppingCartAddedId()
    {
        var shoppingCartDto = _fixture.Create<ShoppingCartDto>();
        var mapData = _mapper.Map<Domain.Entities.ShoppingCart>(shoppingCartDto);
        _context.ShoppingCarts.Add(mapData);
        _context.SaveChanges();
        /// Act
        var result = _services.AddShoppingCart(shoppingCartDto);
        
        // Assert
        _context.ShoppingCarts.Count().Should().Be(result);
    }

    [Fact]
    public async Task DeleteShoppingCart_WhereObjectIsExists_ShouldDeleteObjectFromContext()
    {
        var detailedShoppingCartDto = _fixture.Create<DetailedShoppingCartDto>();
        var mapData = _mapper.Map<Domain.Entities.ShoppingCart>(detailedShoppingCartDto);
        _context.ShoppingCarts.AddRange(mapData);
        _context.SaveChanges();

        /// Act
          _services.DeleteShoppingCart(detailedShoppingCartDto.Id);

        // Assert
        _context.ShoppingCarts.Count().Should().Be(0);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
