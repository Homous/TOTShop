using Application.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.ProuductServices
{
    public interface IProductServices
    {
        List<MiniProductDto> miniDetailsProducts();
        DetailedProductDto GetProductById(int id);
        List<MiniProductDto> GetProductByName(string name);
        void UpdateProduct(AddProductDto updateProductDto);
        void DeleteProduct(int id);
        void AddProduct(AddProductDto addProductDto);
    }
}
