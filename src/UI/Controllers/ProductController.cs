using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Validations;
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
        try
        {
            Log.Information($"parameters: {filter.ToString()}");

            var products = _productServices.MiniDetailsProducts(filter);
            Log.Information($"returned product list count {products.Count}");

            return Ok(new PagedResponse<List<MiniProductDto>>(products, filter.PageNumber, filter.PageSize));
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return BadRequest(new ResultModel()
            {
                Message = "Error",
                Status = false
            });
        }
    }

    [HttpGet("{id}")]
    public IActionResult ProductById(int id)
    {
        try
        {
            Log.Information($"parameters: id: {id}");

            var product = _productServices.GetProductById(id);
            if (product == null)
            {
                Log.Information($"Product with {id} is null");
                return NotFound(new ResultModel()
                {
                    Message = "Product NotFound",
                    Status = false
                });
            }
            return Ok(new ResultModel("", true, product));
        }

        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return BadRequest(new ResultModel()
            {
                Message = "Error",
                Status = false
            });
        }

    }

    [HttpGet("filter/{search}")]
    public IActionResult FilteringData(string? search, [FromQuery] PaginationFilter filter)
    {
        try
        {
            Log.Information($"parameters: search: {search} , filter: {filter.ToString()}");
            var products = _productServices.FilteringData(search, filter);

            if (products != null)
            {
                Log.Information($"Products count {products.Count}");
                return Ok(new PagedResponse<List<MiniProductDto>>(products, filter.PageNumber, filter.PageSize));
            }

            Log.Information($"Products is null");

            return NotFound(new ResultModel()
            {
                Message = "Product not founded",
                Status = true
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return BadRequest(new ResultModel()
            {
                Message = "Error",
                Status = false
            });
        }
    }

    [HttpPost]
    [ServiceFilter(typeof(ModelValidation))]
    public IActionResult AddProduct([FromBody] AddProductDto addProduct)
    {
        try
        {
            Log.Information($"parameters: AddProductDto: {addProduct.ToString()}");

            _productServices.AddProduct(addProduct);
            return Ok(new ResultModel("", true, addProduct));
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return BadRequest(new ResultModel()
            {
                Message = "Error",
                Status = false
            });
        }
    }

    [HttpPut]
    [ServiceFilter(typeof(ModelValidation))]
    public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
    {
        try
        {
            Log.Information($"parameters: UpdateProductDto: {updateProductDto.ToString()}");

            _productServices.UpdateProduct(updateProductDto);
            return Ok(new ResultModel("", true, updateProductDto));
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return BadRequest(new ResultModel()
            {
                Message = "Error",
                Status = false
            });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            Log.Information($"Id: {id}");

            var isDeleted = _productServices.DeleteProduct(id);
            if (isDeleted)
            {
                Log.Information($"Product with id: {id} is deleted");

                return Ok(new ResultModel()
                {
                    Message = "Product Was Deleted",
                    Status = true
                });
            }

            Log.Information($"Product with id: {id} is not found");

            return NotFound(new ResultModel()
            {
                Message = "Product not founded",
                Status = true
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return BadRequest(new ResultModel()
            {
                Message = "Error",
                Status = false
            });
        }
    }


}
