using Application.Dtos.ProductDtos;
using Application.Wrappers;

namespace Application.Contracts.ProuductServices;

public interface IProductServices
{
    List<MiniProductDto> MiniDetailsProducts(PaginationFilter filter);
    DetailedProductDto GetProductById(int? id);
    List<MiniProductDto> FilteringData(string? data, PaginationFilter filter);
    bool UpdateProduct(UpdateProductDto updateProductDto);
    bool DeleteProduct(int? id);
    bool AddProduct(AddProductDto addProductDto);
}
