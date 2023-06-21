using Application.Dtos.ShoppingCart;

namespace Application.Contracts;

public interface IShoppingCartServices
{
    public List<DetailedShoppingCartDto> GetShoppingCarts();
    public DetailedShoppingCartDto GetShoppingCart(int id);
    public int AddShoppingCart(ShoppingCartDto item);
    public void DeleteShoppingCart(int id);
    public bool EditShoppingCart(DetailedShoppingCartDto shoppingCartDto);
}
