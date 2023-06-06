using Application.Contracts;
using Application.Dtos.ShoppingCartItemDto;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers.ShoppingCartItem
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
        public IEnumerable<ShoppingCartItemDto> Get()
        {
            return _shoppingCartItemServices.GetShoppingCartItems();
        }

        [HttpPost("AddShoppingCartItem")]
        public ActionResult Add([FromBody] ShoppingCartItemDto item)
        {
            var id = _shoppingCartItemServices.AddShoppingCartItem(item);

            return Ok($"item created with id {id}");
        }



    }
}
