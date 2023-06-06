using Application.Contracts;
using Application.Dtos.ShoppingCartItem;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemsController : ControllerBase
    {
        private readonly IShoppingCartItemServices _shoppingCartItemServices;

        public ShoppingCartItemsController(IShoppingCartItemServices shoppingCartItemServices)
        {
            _shoppingCartItemServices = shoppingCartItemServices;
        }

        [HttpGet(Name = "GetShoppingCartItems")]
        public IEnumerable<DetailedShoppingCartItemDto> Get()
        {
            return _shoppingCartItemServices.GetShoppingCartItems();
        }

        [HttpGet("{id}")]
        public DetailedShoppingCartItemDto GetItem(int id)
        {
            return _shoppingCartItemServices.GetShoppingCartItem(id);
        }

        [HttpPost("AddShoppingCartItem")]
        public ActionResult Add([FromBody] ShoppingCartItemDto item)
        {
            var id = _shoppingCartItemServices.AddShoppingCartItem(item);

            return Ok($"item created with id {id}");
        }


        [HttpDelete("DeleteShoppingCartItem")]
        public ActionResult Delete(int id)
        {
            _shoppingCartItemServices.DeleteShoppingCartItem(id);

            return Ok($"Item deleted successfully");
        }

        [HttpPost("UpdateShoppingCartItem")]
        public ActionResult UpdateShoppingCartItem(UpdateShoppingCartItemDto item)
        {
            _shoppingCartItemServices.EditShoppingCartItem(item);

            return Ok($"Item updated successfully");
        }

    }
}
