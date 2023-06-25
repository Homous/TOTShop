using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace UI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly IProductServices _productServices;

    public ProductController(IProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpGet]
    public IActionResult MiniDetailsProducts([FromQuery] PaginationFilter filter)
    {
        var products = _productServices.MiniDetailsProducts(filter);

        Log.Information("HttpGet with action:MiniDetailsProducts return: OK");
        return Ok(new PagedResponse<List<MiniProductDto>>(products, filter.PageNumber, filter.PageSize));
    }

    [HttpGet("{id}")]
    public IActionResult ProductById(int id)
    {
        var product = _productServices.GetProductById(id);
        if (product == null)
        {
            Log.Information($"HttpGet/{id} with action:ProductById return: NotFound");
            return NotFound(new ResultModel()
            {
                Message = "Product NotFound",
                Status = false
            });
        }
        Log.Information($"HttpGet/{id} with action:ProductById return: OK");
        return Ok(new ResultModel("", true, product));

    }

    [HttpGet("filter/{search}")]
    public IActionResult FilteringData(string? search, [FromQuery] PaginationFilter filter)
    {
        var products = _productServices.FilteringData(search, filter);

        if (products != null)
        {
            Log.Warning($"HttpGet/filter/{search} with action:FilteringData return: OK");
            return Ok(new PagedResponse<List<MiniProductDto>>(products, filter.PageNumber, filter.PageSize));
        }

        Log.Warning($"HttpGet/filter/{search} with action:FilteringData return: NotFound");

        return NotFound(new ResultModel()
        {
            Message = "Product not founded",
            Status = true
        });
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] AddProductDto addProduct)
    {

        _productServices.AddProduct(addProduct);
        Log.Information("HttpPost with action:AddProduct return: OK");
        return Ok(new ResultModel("", true, addProduct));

    }

    [HttpPut]
    public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
    {
        _productServices.UpdateProduct(updateProductDto);

        Log.Information("HttpPut with action:UpdateProduct return: OK");
        return Ok(new ResultModel("", true, updateProductDto));

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {

        var isDeleted = _productServices.DeleteProduct(id);
        if (isDeleted)
        {
            Log.Information("HttpDelete with action:DeleteProduct return: OK");
            return Ok(new ResultModel()
            {
                Message = "Product Was Deleted",
                Status = true
            });
        }

        Log.Information("HttpDelete with action:DeleteProduct return: NotFound");

        return NotFound(new ResultModel()
        {
            Message = "Product not founded",
            Status = true
        });
    }
}
