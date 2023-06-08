using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult MiniDetailsProducts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(productServices.MiniDetailsProducts());
        }
        [HttpGet("{id:int}")]
        public IActionResult ProudactById( int id)
        {
            try 
            { 
                return Ok(productServices.GetProductById(id));
            }

            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("{sreach}")]
        public IActionResult Search( string search)
        {
            try
            {
                return Ok(productServices.Search(search));
            }
            catch 
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult AddProduct([FromBody] AddProductDto addProduct)
        {
            try
            {
                productServices.AddProduct(addProduct);
                return Ok("Product Was Added");
            }
            catch 
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult UpdateProduct( UpdateProductDto updateProductDto)
        {
            try
            {
                productServices.UpdateProduct(updateProductDto);
                return Ok("Was Updated");
            }
            catch 
            { 
                return BadRequest();
            }
        }
        [HttpDelete]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            try
            {
                productServices.DeleteProduct(id);
                return NoContent();
            }
            catch 
            {
                return BadRequest(); 
            }
        }


    }
}
