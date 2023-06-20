using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UI.ActionResults;

//
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

    [HttpGet(Name = "GetShoppingCart")]
    public IActionResult Get()
    {
        try
        {
            var shoppingCarts = _shoppingCartServices.GetShoppingCarts();
            Log.Information($"returned shoppingCarts list count {shoppingCarts.Count}");
            return Ok(new ActionResultModel()
            {
                Message = shoppingCarts != null ? "Shopping carts returned successfully" : "No data exists",
                Status = true,
                Data = shoppingCarts
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
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
            Log.Information($"return shopping cart id = {id}");
            return Ok(new ActionResultModel()
            {
                Message = shoppingCart != null ? "Shopping cart returned successfully" : "No data exists",
                Status = true,
                Data = shoppingCart
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
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
            Log.Information($"add shoppingCart with data = {item}");
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
            Log.Error(ex.Message, ex);
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
            Log.Information($"Delete ShoppingCart with id = {id}");
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
            Log.Error(ex.Message, ex);
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error {ex.Message}",
                Status = false,
                Data = new Object()
            });
        }

    }

    /* [HttpPut("{id}",Name ="UpdateItems")]
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
            Log.Information($"add item = {item} to ShoppingCart id = {id}");
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
            Log.Error(ex.Message, ex);
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error {ex.Message}",
                Status = false,
                Data = item
            });
        }
    }
}
