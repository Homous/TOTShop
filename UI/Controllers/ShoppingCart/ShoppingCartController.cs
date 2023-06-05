using Application.Contracts;
using Application.Dtos.ShoppingCartDto;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers.ShoppingCart
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
        public IEnumerable<ShoppingCartDto> Get()
        {
            return _shoppingCartServices.GetShoppingCarts();
        }
    }
}
