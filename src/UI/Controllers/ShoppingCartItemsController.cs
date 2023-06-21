using Application.Contracts;
using Application.Dtos.ShoppingCartItem;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UI.ActionResults;

namespace UI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartItemsController : ControllerBase
{
    private readonly IShoppingCartItemServices _shoppingCartItemServices;

    public ShoppingCartItemsController(IShoppingCartItemServices shoppingCartItemServices)
    {
        _shoppingCartItemServices = shoppingCartItemServices;
    }

    [HttpGet]
    public IEnumerable<DetailedShoppingCartItemDto> Get()
    {
        try
        {
            var shoppingCartItems = _shoppingCartItemServices.GetShoppingCartItems();
            Log.Information("HttpGet with action:Get return: OK");
            return shoppingCartItems;
        }
        catch (Exception ex)
        {
            Log.Error("HttpGet with action:Get return: BadRequest", ex.ToString());
            return null;
        }
    }

    [HttpGet("{id}")]
    public DetailedShoppingCartItemDto GetItem(int id)
    {
        try
        {
            
            var shoppingCartItem = _shoppingCartItemServices.GetShoppingCartItem(id);
            Log.Information("HttpGet with action:GetItem return: OK");
            return shoppingCartItem;
        }
        catch (Exception ex)
        {
            Log.Error("HttpGet with action:GetItem return: BadRequest", ex.ToString());
            return null;
        }
    }

    [HttpPost("Add")]
    public ActionResult Add([FromBody] ShoppingCartItemDto item)
    {
        try
        {
            var id = _shoppingCartItemServices.AddShoppingCartItem(item); 
            Log.Information("HttpPost with action:Add return: OK");
            return Ok($"item created with id {id}");
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


    [HttpDelete]
    public ActionResult Delete(int id)
    {
        try
        {
            _shoppingCartItemServices.DeleteShoppingCartItem(id);
            Log.Information("HttpDelete with action:Delete return: OK");
            return Ok($"Item deleted successfully");
        }
        catch (Exception ex)
        {
            Log.Error("HttpDelete with action:Delete return: BadRequest", ex.ToString());
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error ",
                Status = false,
                Data = ""
            });
        }
    }

    [HttpPost]
    public ActionResult UpdateShoppingCartItem(UpdateShoppingCartItemDto item)
    {
        try
        {
            _shoppingCartItemServices.EditShoppingCartItem(item);
            Log.Information("HttpPost with action:UpdateShoppingCartItem return: OK");
            return Ok($"Item updated successfully");
        }
        catch(Exception ex)
        {
            Log.Error("HttpPost with action:UpdateShoppingCartItem return: BadRequest", ex.ToString());
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error ",
                Status = false,
                Data = ""
            });
        }
    }

}
