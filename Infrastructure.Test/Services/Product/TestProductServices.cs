using Application.Dtos.ProductDtos;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.DB;
using Infrastructure.Services.ProductServices;
using Infrastructure.Services.ShoppingCartServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Infrastructure.Test.Services.Product
{
    public class TestProductServices : IDisposable
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly Mapper _mapper;
        private readonly ProductServices _services;

        public TestProductServices() 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _applicationDb = new ApplicationDbContext(options);
            _applicationDb.Database.EnsureCreated();

            var mockAutoMapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductProfile());
            }).CreateMapper().ConfigurationProvider;

            _mapper = new Mapper(mockAutoMapper);
            _services = new ProductServices(_applicationDb, _mapper);
        }

        [Fact]
        public void Get_ShouldReturnProducts_WhenDataFound()
        {
            //Arrange
            var mockData = ProductMockData.GetProducts();
            _applicationDb.Products.AddRange(_mapper.Map<List<Domain.Entities.Product>>(mockData));
            _applicationDb.SaveChanges();

            //Act
            var result = _services.MiniDetailsProducts();
            //Assert
            Assert.Equal(result.Count, mockData.Count);
        }

        [Fact]
        public void GetById_ShouldReturnProduct_WhenDataFound()
        {
            //Arrange
            var mockData = ProductMockData.GetProductById();
            _applicationDb.Products.Add(_mapper.Map<Domain.Entities.Product>(mockData));
            _applicationDb.SaveChanges();

            //Act
            var result = _services.GetProductById(5);
            //Assert
            Assert.Equal(result.Id,mockData.Id);
        }

        [Fact]
        public void Add_ShouldAddProduct_WhenDataValid()
        {
            //Arrange
            var mockData = ProductMockData.AddProduct();
            var product = _applicationDb.Products.Add(_mapper.Map<Domain.Entities.Product>(mockData));
            _applicationDb.SaveChanges();
           
                //Act
               var result= _services.AddProduct(mockData);
                //Assert
                Assert.True(result);
        }

        [Fact]
        public void Filter_ShouldReturnProducts_WhenDataFound()
        {
            //Arrange
            string name = "pro";
            var mockData = ProductMockData.GetProducts();
             _applicationDb.Products.AddRange(_mapper.Map<List<Domain.Entities.Product>>(mockData));
            _applicationDb.SaveChanges();

            //Act
            var result = _services.FilteringData(name);
            //Assert
            Assert.Equal(result.Count, mockData.Count);
        }

        [Fact]
        public void Delete_ShouldDeleteProduct_WhenIdFound()
        {
            //Arrange
            var mockData = ProductMockData.UpdateProduct();
             _applicationDb.Products.AddRange(_mapper.Map<Domain.Entities.Product>(mockData));
            _applicationDb.SaveChanges();

            //Act
            var result = _services.DeleteProduct(mockData.Id);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void Update_ShouldUpdateProduct_WhenIdFound()
        {
            //Arrange
            var mockData = ProductMockData.UpdateProduct();
            _applicationDb.Products.AddRange(_mapper.Map<Domain.Entities.Product>(mockData));
            _applicationDb.SaveChanges();

            //Act
            var result = _services.UpdateProduct(mockData);

            //Assert
            Assert.True(result);
        }


        public void Dispose()
        {
            _applicationDb.Database.EnsureCreated();
            _applicationDb.Dispose();
        }
    }
}
