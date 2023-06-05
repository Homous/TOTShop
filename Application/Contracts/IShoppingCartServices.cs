using Application.Dtos.ShoppingCartDto;

namespace Application.Contracts
{
    public interface IShoppingCartServices
    {
        public List<ShoppingCartDto> GetShoppingCarts();
        public ShoppingCartDto GetShoppingCart(int id);
        public void AddShoppingCart(ShoppingCartDto item);
        public void DeleteShoppingCart(int id);
        public void EditShoppingCart(int id, ShoppingCartDto shoppingCartDto);
    }
}
