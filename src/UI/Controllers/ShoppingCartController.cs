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
        try
        {

            var shoppingCarts = _shoppingCartServices.GetShoppingCarts();
            Log.Information("HttpGet with action:Get return: OK");
            return Ok(new ActionResultModel()
            {
                Message = shoppingCarts != null ? "Shopping carts returned successfully" : "No data exists",
                Status = true,
                Data = shoppingCarts
            });
        }
        catch (Exception ex)
        {
            Log.Error("HttpGet with action:Get return: BadRequest", ex.ToString());
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error ",
                Status = false,
                Data = ""
            }); ;
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetItemById(int id)
    {
        try
        {
            var shoppingCart = _shoppingCartServices.GetShoppingCart(id);
            Log.Information("HttpGet with action:GetItem return: OK");
            return Ok(new ActionResultModel()
            {
                Message = shoppingCart != null ? "Shopping cart returned successfully" : "No data exists",
                Status = true,
                Data = shoppingCart
            });
        }
        catch (Exception ex)
        {
            Log.Error("HttpGet with action:GetItem return: BadRequest", ex.ToString());
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error",
                Status = false,
                Data = ""
            });
        }
    }

    [HttpPost]
    public IActionResult Add([FromBody] ShoppingCartDto item)
    {
        try
        {
            var id = _shoppingCartServices.AddShoppingCart(item);
            if (id == -1)
            {
                Log.Error("HttpPost with action:Add return: BadRequest");
                return BadRequest(new ActionResultModel()
                {
                    Message = $"Data is not valid",
                    Status = false,
                    Data = ""
                });
            }

            Log.Information("HttpPost with action:Add return: OK");
            return Ok(new ActionResultModel()
            {
                Message = $"Shopping cart added with Id {id}",
                Status = true,
                Data = id
            });
        }
        catch (Exception ex)
        {
            Log.Error("HttpPost with action:Add return: BadRequest", ex.ToString());
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error ",
                Status = false,
                Data = ""
            });
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            Log.Information($"Delete ShoppingCart with id = {id}");
            _shoppingCartServices.DeleteShoppingCart(id);
            Log.Information("HttpDelete with action:Delete return: OK");
            return Ok(new ActionResultModel()
            {
                Message = $"Shopping cart deleted with Id {id}",
                Status = true,
                Data = ""
            });
        }
        catch (Exception ex)
        {
            Log.Error("HttpDelete with action:Delete return: BadRequest", ex.ToString());
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error ",
                Status = false,
                Data = new Object()
            });
        }

    }


    [HttpPut("{id}")]
    public ActionResult AddShoppingCartItemOnShoppingCart(int id, [FromBody] DetailedShoppingCartDto cartDto)
    {
        try
        {
            if (id != cartDto.Id)
            {
                Log.Information("HttpPut with action:AddShoppingCartItemOnShoppingCart return:BadRequest");
                return BadRequest("Ids not matching");
            }
            var confirmEditTransaction = _shoppingCartServices.EditShoppingCart(cartDto);

            if (confirmEditTransaction)
            {
                var editedCart = _shoppingCartServices.GetShoppingCart(id);
                Log.Information("HttpPut with action:AddShoppingCartItemOnShoppingCart return:Ok");
                return Ok(new ActionResultModel()
                {
                    Message = $"Shopping cart updated with Id {id}",
                    Status = true,
                    Data = editedCart
                });
            }

            Log.Information("HttpPut with action:AddShoppingCartItemOnShoppingCart return:BadRequest");
            return BadRequest(new ActionResultModel() { Data = cartDto, Message = "Data is not valid", Status = false });
        }
        catch (Exception ex)
        {
            Log.Error("HttpPut with action:AddShoppingCartItemOnShoppingCart return: BadRequest", ex.ToString());
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error ",
                Status = false,
                Data = cartDto
            });
        }
    }
}
