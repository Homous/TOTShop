using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Mapping;
using Application.Validations;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.Test.WebApi;
using System.IO.Pipes;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;

        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        [HttpGet]
        [ServiceFilter(typeof(ModelValidation))]
        public IActionResult MiniDetailsProducts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var products = productServices.MiniDetailsProducts();
                return Ok(new
                {
                    Message = "Products returned",
                    IsDone = true
                });
            }
            catch
            {
                return BadRequest(new
                {
                    Message = "Error",
                    IsDone = false,
                    ProductPage = 0
                });
            }
        }
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(ModelValidation))]
        public IActionResult ProudactById( int id)
        {
            try 
            { 
                var product = productServices.GetProductById(id);
                return Ok(new
                {
                    Message = "Product returned",
                    IsDone = true,
                    Product = product
                });
            }

            catch
            {
                return NotFound(new
                {
                    Message = "Product not founded",
                    IsDone = false,
                    ProductPage = 0
                });
            }

        }
        [HttpGet("{search}")]
        [ServiceFilter(typeof(ModelValidation))]
        public IActionResult Search(string search)
        {
            try
            {
                productServices.Search(search);
                return Ok(new
                {
                    Message = "Products returned",
                    IsDone = true
                });
            }
            catch 
            {
                return NotFound(new
                {
                    Message = "Product not founded",
                    IsDone = false,
                    ProductPage = 0
                });
            }
        }
        [HttpPost]
        [ServiceFilter(typeof(ModelValidation))]
        public IActionResult AddProduct([FromBody] AddProductDto addProduct)
        {
            try
            {
                productServices.AddProduct(addProduct);
                return Ok(new
                {
                    Message = "Product Was Added.",
                    IsDone = true,
                    Product = addProduct
                });
            }
            catch 
            {
                return BadRequest(new
                {
                    Message = "Error",
                    IsDone = false
                });
            }
        }
        [HttpPut]
        [ServiceFilter(typeof(ModelValidation))]
        public IActionResult UpdateProduct( UpdateProductDto updateProductDto)
        {
            try
            {
                productServices.UpdateProduct(updateProductDto);
                return Ok(new
                {
                    Message = "Product Was Updated.",
                    IsDone = true,
                    Product = updateProductDto
                });
            }
            catch 
            {
                return BadRequest(new
                {
                    Message = "Error",
                    IsDone = false
                });
            }
        }
        [HttpDelete]
        [ServiceFilter(typeof(ModelValidation))]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            try
            {
                productServices.DeleteProduct(id);
                return Ok(new
                {
                    Message = "Product Was Deleted.",
                    IsDone = true
                });
            }
            catch 
            {
                return BadRequest(new
                {
                    Message = "Error",
                    IsDone = false
                });
            }
        }


    }
}
