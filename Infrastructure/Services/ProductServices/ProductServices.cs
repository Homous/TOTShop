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
                var getWithMap = mapper.Map<Product>(addProductDto);
                db.Products.Add(getWithMap);
                db.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            var getById = db.Products.FirstOrDefault(x => x.Id == id);
            if (getById != null)
            {
                db.Products.Remove(getById);
                db.SaveChanges();
            }
        }

        public DetailedProductDto GetProductById(int id)
        {
            var getById = db.Products.Find(id);
            var getMap = mapper.Map<DetailedProductDto>(getById);
                return getMap;
        }

        public List<MiniProductDto> Search(string search)
        {
            var filter = db.Products.Where(n => n.Name.Contains(search) || n.Description.Contains(search)).ProjectTo<MiniProductDto>(mapper.ConfigurationProvider);
            var getList = filter.ToList();
           
            return getList;
        }

        public List<MiniProductDto> miniDetailsProducts()
        {
            var getMiniPro = db.Products.ProjectTo<MiniProductDto>(mapper.ConfigurationProvider);
            var getList = getMiniPro.ToList();
            
            return getList;
        }

        public void UpdateProduct( UpdateProductDto updateProductDto)
        {
            if (updateProductDto != null)
            {
                var get = mapper.Map<Product>(updateProductDto);
                db.Products.Update(get);
                db.SaveChanges();
            }
        }
    }
}
