using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Mapping;
using Application.Wrappers;
using AutoFixture;
using AutoMapper;
using Infrastructure.DB;
using Infrastructure.Services.ProductServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Services.Product;

public class TestProductServices : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly Mapper _mapper;
    private readonly IProductServices _services;
    private Fixture _fixture;

    public TestProductServices()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();

        var mockAutoMapper = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ProductProfile());
        }).CreateMapper().ConfigurationProvider;

        _mapper = new Mapper(mockAutoMapper);
        _services = new ProductServices(_context, _mapper);
        _fixture = new Fixture();
    }

    [Fact]
    public void Get_WhenDataFound_ShouldReturnProducts()
    {
        //Arrange
        var paging = new PaginationFilter();
        var detailedProductDtoList = _fixture.CreateMany<DetailedProductDto>(3).ToList();
        _context.Products.AddRange(_mapper.Map<List<Domain.Entities.Product>>(detailedProductDtoList));
        _context.SaveChanges();

        //Act
        var result = _services.MiniDetailsProducts(paging);
        //Assert
        Assert.Equal(result.Count, detailedProductDtoList.Count);
    }

    [Fact]
    public void GetById_WhenDataFound_ShouldReturnProduct()
    {
        //Arrange        
        var detailedProductDto = _fixture.Create<DetailedProductDto>();
        _context.Products.Add(_mapper.Map<Domain.Entities.Product>(detailedProductDto));
        _context.SaveChanges();

        //Act
        var result = _services.GetProductById(detailedProductDto.Id);

        //Assert
        Assert.Equal(result.Id, detailedProductDto.Id);
    }

    [Fact]
    public void Add_WhenDataValid_ShouldAddProduct()
    {
        //Arrange
        var addProductDto = _fixture.Create<AddProductDto>();
        var product = _context.Products.Add(_mapper.Map<Domain.Entities.Product>(addProductDto));
        _context.SaveChanges();

        //Act
        var result = _services.AddProduct(addProductDto);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void Filter_WhenDataFound_ShouldReturnProducts()
    {
        //Arrange
        var detailedProductDto = _fixture.Create<DetailedProductDto>();
        var name = detailedProductDto.Name;
        var paging = new PaginationFilter(1, 1);
        _context.Products.AddRange(_mapper.Map<Domain.Entities.Product>(detailedProductDto));
        _context.SaveChanges();

        //Act
        var result = _services.FilteringData(name, paging);

        //Assert
        Assert.Equal(1, result.Count);
    }

    [Fact]
    public void Delete_WhenIdFound_ShouldDeleteProduct()
    {
        //Arrange
        var updateProductDto = _fixture.Create<UpdateProductDto>();
        _context.Products.AddRange(_mapper.Map<Domain.Entities.Product>(updateProductDto));
        _context.SaveChanges();

        //Act
        var result = _services.DeleteProduct(updateProductDto.Id);

        //Assert
        Assert.True(result);
    }
    [Fact]
    public void Update_ShouldUpdateProduct_WhenIdFound()
    {
        //Arrange
        var detailedProductDtoList = _fixture.CreateMany<DetailedProductDto>(3).ToList();
        _context.Products.AddRange(_mapper.Map<List<Domain.Entities.Product>>(detailedProductDtoList));
        _context.SaveChanges();

        var updateProductDto = _fixture.Create<UpdateProductDto>();
        updateProductDto.Id = detailedProductDtoList.First().Id;

        //Act
        var result = _services.UpdateProduct(updateProductDto);

        //Assert
        Assert.True(result);
    }


    public void Dispose()
    {
        _context.Database.EnsureCreated();
        _context.Dispose();
    }
}
