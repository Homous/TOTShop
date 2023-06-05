using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
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

        public ProductServices(ApplicationDbContext db) 
        {
            this.db = db;
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
            throw new NotImplementedException();
        }

        public List<MiniProductDto> miniDetailsProducts()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(AddProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
