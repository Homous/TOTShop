using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Infrastructure.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProductServices(ApplicationDbContext db,IMapper mapper) 
        {
            this.db = db;
            this.mapper = mapper;
        }
        public void AddProduct( AddProductDto addProductDto)
        {
            if(addProductDto != null)
            {
                var map = mapper.Map<Product>(addProductDto);
                db.Products.Add(map);
                db.SaveChanges();
            }
        }

        public bool DeleteProduct(int? id)
        {
            var getProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if (getProduct != null)
            {
                db.Products.Remove(getProduct);
                db.SaveChanges();
                return true;
            }
            
            return false;
        }

        public DetailedProductDto GetProductById(int? id)
        {
            var getProduct = db.Products.Find(id);
            var map = mapper.Map<DetailedProductDto>(getProduct);
                return map;
        }

        public List<MiniProductDto> Search(string? filter)
        {
            var getProducts = db.Products.Where(n => n.Name.Contains(filter) || n.Description.Contains(filter)).ProjectTo<MiniProductDto>(mapper.ConfigurationProvider);
            var getList = getProducts.ToList();
           
            return getList;
        }

        public List<MiniProductDto> MiniDetailsProducts()
        {
                var getProducts = db.Products.ProjectTo<MiniProductDto>(mapper.ConfigurationProvider);
                var getList = getProducts.ToList();
                return getList;
        }

        public void UpdateProduct( UpdateProductDto updateProductDto)
        {
            if (updateProductDto != null)
            {
                var map = mapper.Map<Product>(updateProductDto);
                db.Products.Update(map);
                db.SaveChanges();
            }
        }
    }
}
