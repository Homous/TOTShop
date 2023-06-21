using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Wrappers;
using Domain.Entities;
using Infrastructure.DB;
using Mapster;
using MapsterMapper;
using Serilog;

namespace Infrastructure.Services.ProductServices;
public class ProductServices : IProductServices
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductServices(ApplicationDbContext db, IMapper mapper)
    {
        _context = db;
        _mapper = mapper;
    }
    public bool AddProduct(AddProductDto addProductDto)
    {
        try
        {
            Log.Information($"AddProduct at ProductService.cs");
            Log.Information($"parameters: {addProductDto.ToString()}");
            if (addProductDto != null)
            {

                var newProduct = _mapper.Map<Product>(addProductDto);
                _context.Products.Add(newProduct);
                _context.SaveChanges();

                Log.Information($"New Product added in database with Id {newProduct.Id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }

    public bool DeleteProduct(int id)
    {
        try
        {
            Log.Information($"DeleteProduct at ProductService.cs");
            Log.Information($"parameters: Id: {id}");

            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                Log.Information($"Product with Id:{id} is deleted");

                return true;
            }

            Log.Warning($"Product with Id:{id} is not found");
            return false;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }

    public DetailedProductDto GetProductById(int id)
    {
        try
        {
            Log.Information($"GetProductById at ProductService.cs");
            Log.Information($"parameters: Id: {id}");

            var product = _context.Products.Find(id);
            if (product == null)
            {
                Log.Error($"Product is not found with Id: {id}");
                return null;
            }

            var productDto = _mapper.Map<DetailedProductDto>(product);
            Log.Information($"Product is found with Id: {id}");

            return productDto;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public List<MiniProductDto> FilteringData(string data, PaginationFilter filter)
    {
        try
        {
            Log.Information($"FilteringData at ProductService.cs");
            Log.Information($"parameters: data: {data}, PaginationFilter: {filter.ToString()}");

            var pagination = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var getProducts = _context.Products.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Where(n => n.Name.Contains(data) || n.Description.Contains(data))
                .ProjectToType<MiniProductDto>();

            var productList = getProducts.ToList();
            Log.Information($"returned with product list count: {productList.Count}");

            return productList;

        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }

    }

    public List<MiniProductDto> MiniDetailsProducts(PaginationFilter filter)
    {
        try
        {
            Log.Information($"MiniDetailsProducts at ProductService.cs");
            Log.Information($"parameters: PaginationFilter: {filter.ToString()}");

            var pagination = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var getProducts = _context.Products.Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectToType<MiniProductDto>();
            var productlist = getProducts.ToList();

            if (productlist == null)
            {
                Log.Warning($"No products exists");
                return productlist;
            }

            Log.Information($"Return with product count: {productlist.Count}");
            return productlist;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public bool UpdateProduct(UpdateProductDto updateProductDto)
    {
        try
        {
            Log.Information($"UpdateProduct at ProductService.cs");
            Log.Information($"parameters: UpdateProductDto: {updateProductDto.ToString()}");

            if (updateProductDto != null)
            {
                var existsProduct = _context.Products.FirstOrDefault(p => p.Id == updateProductDto.Id);
                if (existsProduct != null)
                {
                    Log.Information($"product with Id: {existsProduct.Id} is exists with properties: {existsProduct.ToString()}");
                    existsProduct.Price = updateProductDto.Price;
                    existsProduct.Name = updateProductDto.Name;
                    _context.Products.Update(existsProduct);
                    _context.SaveChanges();

                    Log.Information($"Product has updated with Id: {existsProduct.Id} with new properties: {existsProduct.ToString()}");
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }
}

