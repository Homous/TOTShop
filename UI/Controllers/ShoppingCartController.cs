using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<DetailedShoppingCartDto> Get()
        {
            return _shoppingCartServices.GetShoppingCarts();
        }

        [HttpGet("{id}")]
        public DetailedShoppingCartDto GetItem(int id)
        {
            return _shoppingCartServices.GetShoppingCart(id);
        }

        [HttpPost("AddShoppingCart")]
        public ActionResult Add([FromBody] ShoppingCartDto item)
        {
            var id = _shoppingCartServices.AddShoppingCart(item);

            return Ok($"Cart created with id {id}");
        }

        [HttpDelete("DeleteShoppingCart")]
        public ActionResult Delete(int id)
        {
            _shoppingCartServices.DeleteShoppingCart(id);

            return Ok($"Cart deleted successfully");
        }

        [HttpPost("UpdateShoppingCart")]
        public ActionResult UpdateShoppingCart(EditShoppingCartItemDto item)
        {
            _shoppingCartServices.EditShoppingCart(item);

            return Ok($"Cart updated successfully");
        }
    }
}
