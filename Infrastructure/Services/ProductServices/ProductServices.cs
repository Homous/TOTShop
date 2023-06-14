using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.DB;

namespace Infrastructure.Services.ProductServices;
public class ProductServices : IProductServices
{
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;

    public ProductServices(ApplicationDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }
    public bool AddProduct(AddProductDto addProductDto)
    {
        if (addProductDto != null)
        {
            var map = mapper.Map<Product>(addProductDto);
            db.Products.Add(map);
            db.SaveChanges();
            return true;
        }
        return false;
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

    public List<MiniProductDto> FilteringData(string? data, PaginationFilter filter)
    {
        var pagination = new PaginationFilter(filter.PageNumber, filter.PageSize);

        var getProducts = db.Products.Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Where(n => n.Name.Contains(data) || n.Description.Contains(data))
            .ProjectTo<MiniProductDto>(mapper.ConfigurationProvider);

        var getList = getProducts.ToList();
        return getList;
    }

    public List<MiniProductDto> MiniDetailsProducts(PaginationFilter filter)
    {
        var pagination = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var getProducts = db.Products.Skip((pagination.PageNumber - 1) * pagination.PageSize)
        .Take(pagination.PageSize)
        .ProjectTo<MiniProductDto>(mapper.ConfigurationProvider);
        var getList = getProducts.ToList();
        return getList;
        //return PagedResponse<List<MiniProductDto>>(getList, filter.PageNumber, filter.PageSize);
    }

    public bool UpdateProduct(UpdateProductDto updateProductDto)
    {
        if (updateProductDto != null)
        {
            var map = mapper.Map<Product>(updateProductDto);
            var productDb = db.Products.FirstOrDefault(p => p.Id == updateProductDto.Id);
            productDb.Price = updateProductDto.Price;
            productDb.Name = updateProductDto.Name;
            db.Products.Update(productDb);
            db.SaveChanges();
            return true;
        }
        return false;
    }
}

