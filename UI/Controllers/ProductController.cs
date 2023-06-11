using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Validations;
//using Application.Validations;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ProudactById(int? id)
        {
            try
            {
                var product = productServices.GetProductById(id);
                if (product == null)
                {
                    return NotFound(new
                    {
                        Message = "Product NotFound",
                        IsDone = true
                    });
                }
                return Ok(new
                {
                    Message = "Product returned",
                    IsDone = true,
                    ProductPage = 1,
                    Product = product
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
        [HttpGet("{search}")]
        [ServiceFilter(typeof(ModelValidation))]
        public IActionResult Search(string? search)
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
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
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
        public IActionResult DeleteProduct([FromQuery] int? id)
        {
            try
            {
                var deleteProduct = productServices.DeleteProduct(id);
                if (deleteProduct != false)
                {
                    return Ok(new
                    {
                        Message = "Product Was Deleted.",
                        IsDone = true
                    });
                }
                return NotFound(new
                {
                    Message = "Product Not Founded.",
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
