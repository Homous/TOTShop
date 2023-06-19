﻿using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Validations;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

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

            var products = _productServices.MiniDetailsProducts(filter);
            
            return Ok(new PagedResponse<List<MiniProductDto>>(products, filter.PageNumber, filter.PageSize));
        }
        catch
        {
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
            var product = _productServices.GetProductById(id);
            if (product == null)
            {
                return NotFound(new ResultModel()
                {
                    Message = "Product NotFound",
                    Status = false
                });
            }
            return Ok(new ResultModel("", true, product));
        }

        catch
        {
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
            var products = _productServices.FilteringData(search, filter);

            if (products != null)
            {
                return Ok(new PagedResponse<List<MiniProductDto>>(products, filter.PageNumber, filter.PageSize));
            }
            return NotFound(new ResultModel()
            {
                Message = "Product not founded",
                Status = true
            });
        }
        catch
        {
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
            _productServices.AddProduct(addProduct);
            return Ok(new ResultModel("", true, addProduct));
        }
        catch
        {
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
            _productServices.UpdateProduct(updateProductDto);
            return Ok(new ResultModel("", true, updateProductDto));
        }
        catch
        {
            return BadRequest(new ResultModel()
            {
                Message = "Error",
                Status = false
            });
        }
    }

    [HttpDelete]
    public IActionResult DeleteProduct([FromQuery] int? id)
    {
        try
        {
            var deleteProduct = _productServices.DeleteProduct(id);
            if (deleteProduct != false)
            {
                return Ok(new ResultModel()
                {
                    Message = "Product Was Deleted",
                    Status = true
                });
            }
            return NotFound(new ResultModel()
            {
                Message = "Product not founded",
                Status = true
            });
        }
        catch
        {
            return BadRequest(new ResultModel()
            {
                Message = "Error",
                Status = false
            });
        }
    }


}
