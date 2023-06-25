using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UI.ActionResults;


namespace UI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartServices _shoppingCartServices;

    public ShoppingCartController(IShoppingCartServices shoppingCartServices)
    {
        _shoppingCartServices = shoppingCartServices;
    }

    [HttpGet]
    public IActionResult Get()
    {
            var shoppingCarts = _shoppingCartServices.GetShoppingCarts();
            return Ok(new ActionResultModel()
            {
                Message = shoppingCarts != null ? "Shopping carts returned successfully" : "No data exists",
                Status = true,
                Data = shoppingCarts
            });
    }

    [HttpGet("{id}")]
    public IActionResult GetItemById(int id)
    {
            var shoppingCart = _shoppingCartServices.GetShoppingCart(id);
            return Ok(new ActionResultModel()
            {
                Message = shoppingCart != null ? "Shopping cart returned successfully" : "No data exists",
                Status = true,
                Data = shoppingCart
            });
    }

    [HttpPost]
    public IActionResult Add([FromBody] ShoppingCartDto item)
    {
        var id = _shoppingCartServices.AddShoppingCart(item);
        if (id == -1)
        {
            return BadRequest(new ActionResultModel()
            {
                Message = $"Data is not valid",
                Status = false,
                Data = ""
            });
        }

            return Ok(new ActionResultModel()
            {
                Message = $"Shopping cart added with Id {id}",
                Status = true,
                Data = id
            });
        }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
            _shoppingCartServices.DeleteShoppingCart(id);
            return Ok(new ActionResultModel()
            {
                Message = $"Shopping cart deleted with Id {id}",
                Status = true,
                Data = ""
            });

    }


    [HttpPut("{id}")]
    public ActionResult AddShoppingCartItemOnShoppingCart(int id, [FromBody] DetailedShoppingCartDto cartDto)
    {
            if (id != cartDto.Id)
            {
                return BadRequest("Ids not matching");
            }
            var confirmEditTransaction = _shoppingCartServices.EditShoppingCart(cartDto);

            if (confirmEditTransaction)
            {
                var editedCart = _shoppingCartServices.GetShoppingCart(id);
                return Ok(new ActionResultModel()
                {
                    Message = $"Shopping cart updated with Id {id}",
                    Status = true,
                    Data = editedCart
                });
            }
            return BadRequest(new ActionResultModel() { Data = cartDto, Message = "Data is not valid", Status = false });
    }
}
