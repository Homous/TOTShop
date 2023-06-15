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
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductServices(ApplicationDbContext db, IMapper mapper)
    {
        _context = db;
        _mapper = mapper;
    }
    public bool AddProduct(AddProductDto addProductDto)
    {
        if (addProductDto != null)
        {
            var map = _mapper.Map<Product>(addProductDto);
            _context.Products.Add(map);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool DeleteProduct(int? id)
    {
        var getProduct = _context.Products.FirstOrDefault(x => x.Id == id);
        if (getProduct != null)
        {
            _context.Products.Remove(getProduct);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public DetailedProductDto GetProductById(int? id)
    {
        var getProduct = _context.Products.Find(id);
        var map = _mapper.Map<DetailedProductDto>(getProduct);
        return map;
    }

    public List<MiniProductDto> FilteringData(string? data, PaginationFilter filter)
    {
        var pagination = new PaginationFilter(filter.PageNumber, filter.PageSize);

        var getProducts = _context.Products.Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Where(n => n.Name.Contains(data) || n.Description.Contains(data))
            .ProjectTo<MiniProductDto>(_mapper.ConfigurationProvider);

        var getList = getProducts.ToList();
        return getList;
    }

    public List<MiniProductDto> MiniDetailsProducts(PaginationFilter filter)
    {
        var pagination = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var getProducts = _context.Products.Skip((pagination.PageNumber - 1) * pagination.PageSize)
        .Take(pagination.PageSize)
        .ProjectTo<MiniProductDto>(_mapper.ConfigurationProvider);
        var getList = getProducts.ToList();
        return getList;
        //return PagedResponse<List<MiniProductDto>>(getList, filter.PageNumber, filter.PageSize);
    }

    public bool UpdateProduct(UpdateProductDto updateProductDto)
    {
        if (updateProductDto != null)
        {
            var map = _mapper.Map<Product>(updateProductDto);
            var productDb = _context.Products.FirstOrDefault(p => p.Id == updateProductDto.Id);
            productDb.Price = updateProductDto.Price;
            productDb.Name = updateProductDto.Name;
            _context.Products.Update(productDb);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}

