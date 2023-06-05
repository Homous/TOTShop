using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using AutoMapper;
using Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void AddProduct(AddProductDto addProductDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public DetailedProductDto GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public List<MiniProductDto> GetProductByName(string name)
        {
            var filter = db.Products.Where(n => n.Name.Contains(name));
            var getList = filter.ToList();
            var getWithMap = mapper.Map<List<MiniProductDto>>(getList);
            return getWithMap;
        }

        public List<MiniProductDto> miniDetailsProducts()
        {
            var getMiniPro =  db.Products.ToList();
            var getWithMap = mapper.Map<List<MiniProductDto>>(getMiniPro);
            return getWithMap;
        }

        public void UpdateProduct(AddProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
