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
            Log.Information($"returned shoppingCartItems list count {shoppingCartItems.Count}");
            return shoppingCartItems;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return null;
        }
    }

    [HttpGet("{id}")]
    public DetailedShoppingCartItemDto GetItem(int id)
    {
        try
        {
            Log.Information($"request to get shoppingCartItem id = {id}");
            var shoppingCartItem = _shoppingCartItemServices.GetShoppingCartItem(id);
            Log.Information($"returned shoppingCartItem  item = {shoppingCartItem}");
            return shoppingCartItem;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return null;
        }
    }

    [HttpPost]
    public ActionResult Add([FromBody] ShoppingCartItemDto item)
    {
        try
        {
            Log.Information($"request to add shoppingCartItem data = {item}");
            var id = _shoppingCartItemServices.AddShoppingCartItem(item);
            Log.Information($"item was added shoppingCartItem id = {id}");
            return Ok($"item created with id {id}");
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
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
            Log.Information($"request to delete shoppingCartItem id = {id}");
            _shoppingCartItemServices.DeleteShoppingCartItem(id);
            Log.Information($"item was deleted id = {id}");
            return Ok($"Item deleted successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message,ex);
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error ",
                Status = false,
                Data = ""
            });
        }
    }

    [HttpPost("UpdateShoppingCartItem")]
    public ActionResult UpdateShoppingCartItem(UpdateShoppingCartItemDto item)
    {
        try
        {
            Log.Information($"request to update shoppingCartItem data = {item}");
            _shoppingCartItemServices.EditShoppingCartItem(item);
            Log.Information($"item was updated id = {item.Id}");
            return Ok($"Item updated successfully");
        }
        catch(Exception ex)
        {
            Log.Error(ex.Message,ex);
            return BadRequest(new ActionResultModel()
            {
                Message = $"Error ",
                Status = false,
                Data = ""
            });
        }
    }

}
