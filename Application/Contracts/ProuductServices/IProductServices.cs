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
        List<MiniProductDto> MiniDetailsProducts();
        DetailedProductDto GetProductById(int? id);
        List<MiniProductDto> FilteringData(string? filter);
        bool UpdateProduct(UpdateProductDto updateProductDto);
        bool DeleteProduct(int? id);
        bool AddProduct(AddProductDto addProductDto);
    }
}
