using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using UI.ActionResults;

//
namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartServices _shoppingCartServices;

        public ShoppingCartController(IShoppingCartServices shoppingCartServices)
        {
            _shoppingCartServices = shoppingCartServices;
        }

        [HttpGet(Name = "GetShoppingCart")]
        public IActionResult Get()
        {
            try
            {
                var shoppingCarts = _shoppingCartServices.GetShoppingCarts();
                return Ok(new ActionResultModel()
                {
                    Message = shoppingCarts != null ? "Shopping carts returned successfully" : "No data exists",
                    Status = true,
                    Data = shoppingCarts
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ActionResultModel()
                {
                    Message = $"Error {ex.Message}",
                    Status = false,
                    Data = ""
                }); ;
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            try
            {
                var shoppingCart = _shoppingCartServices.GetShoppingCart(id);
                return Ok(new ActionResultModel()
                {
                    Message = shoppingCart != null ? "Shopping cart returned successfully" : "No data exists",
                    Status = true,
                    Data = shoppingCart
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ActionResultModel()
                {
                    Message = $"Error {ex.Message}",
                    Status = false,
                    Data = ""
                });
            }
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] ShoppingCartDto item)
        {
            try
            {
                var id = _shoppingCartServices.AddShoppingCart(item);
                return Ok(new ActionResultModel()
                {
                    Message = $"Shopping cart added with Id {id}",
                    Status = true,
                    Data = id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ActionResultModel()
                {
                    Message = $"Error {ex.Message}",
                    Status = false,
                    Data = ""
                });
            }
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                _shoppingCartServices.DeleteShoppingCart(id);
                return Ok(new ActionResultModel()
                {
                    Message = $"Shopping cart deleted with Id {id}",
                    Status = true,
                    Data = ""
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ActionResultModel()
                {
                    Message = $"Error {ex.Message}",
                    Status = false,
                    Data = new Object()
                });
            }

        }
        /*
                [HttpPut("{id}")]
                public ActionResult UpdateShoppingCart(int id, EditShoppingCartItemDto item)
                {
                    try
                    {
                        if (id != item.Id)
                            return BadRequest("Ids not matching");

                        _shoppingCartServices.EditShoppingCart(item);
                        return Ok(new
                        {
                            Message = $"Shopping cart updated with Id {item.Id}",
                            Status = true,
                            Data = item
                        });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new
                        {
                            Message = $"Error {ex.Message}",
                            Status = false,
                            Data = item
                        });
                    }
                }*/

        [HttpPut("{id}")]
        public ActionResult AddShoppingCartItemOnShoppingCart(int id, [FromBody] DetailedShoppingCartDto item)
        {
            try
            {
                if (id != item.Id)
                    return BadRequest("Ids not matching");

                _shoppingCartServices.EditShoppingCart(item);
                return Ok(new ActionResultModel()
                {
                    Message = $"Shopping cart updated with Id {item.Id}",
                    Status = true,
                    Data = item
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ActionResultModel()
                {
                    Message = $"Error {ex.Message}",
                    Status = false,
                    Data = item
                });
            }
        }
    }
}
