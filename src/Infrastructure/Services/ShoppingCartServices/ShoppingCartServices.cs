using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Domain.Entities;
using Infrastructure.DB;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Services.ShoppingCartServices;

public class ShoppingCartServices : IShoppingCartServices
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ShoppingCartServices(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public int AddShoppingCart(ShoppingCartDto cartDto)
    {
        try
        {
            Log.Information("AddShoppingCart at ShoppingCartServices.cs");
            Log.Information($"parameters: {cartDto.ToString()}");

            var cart = _mapper.Map<ShoppingCart>(cartDto);

            bool isValid = CartValidationProcess(cart);
            if (isValid)
            {
                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges();

                Log.Information($"cart was added with id  = {cart.Id}, with totalCost: {cart.TotalCost}");
                return cart.Id;
            }

            Log.Information($"cart is not valid - was added with id  = {cart.Id}, with totalCost: {cart.TotalCost}");
            return -1;
        }
        catch (Exception ex)
        {

            Log.Error(ex.Message, ex);
            return -1;
        }
    }
    public bool EditShoppingCart(DetailedShoppingCartDto shoppingCartDto)
    {
        try
        {
            Log.Information("EditShoppingCart at ShoppingCartServices.cs");
            Log.Information($"parameters: {shoppingCartDto.ToString()}");

            var cart = _mapper.Map<ShoppingCart>(shoppingCartDto);
            var cartDatabase = _context.ShoppingCarts.Include(x => x.ShoppingCartItems).FirstOrDefault(x => x.Id == shoppingCartDto.Id);

            if (cartDatabase == null)
            {
                Log.Information($"cart with Id {shoppingCartDto.Id} not exists in database");
                return false;
            }

            Log.Information($"cart with Id {shoppingCartDto.Id} exists in database");
            var isValid = CartValidationProcess(cart);
            if (!isValid)
            {
                Log.Warning($"cart with Id {shoppingCartDto.Id} has not valid totalCost: {cart.TotalCost}");
                return false;
            }

            Log.Information($"cart with Id {shoppingCartDto.Id} has valid totalCost: {cart.TotalCost}");
            cartDatabase.ShoppingCartItems.RemoveAll(x => true);
            cartDatabase.ShoppingCartItems = cart.ShoppingCartItems;
            _context.SaveChanges();
            Log.Information($"cart with Id {shoppingCartDto.Id} has edited in database");
            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }
    private bool CartValidationProcess(ShoppingCart cart)
    {
        var isValid = true;

        if (isValid)
            isValid = ValidateTotalCost(cart);

        if (isValid)
            isValid = ValidateQuantity(cart);

        return isValid;
    }
    private bool ValidateQuantity(ShoppingCart cart)
    {
        try
        {
            var isValid = true;
            Log.Information("ValidateQuantity at ShoppingCartServices.cs");
            cart.ShoppingCartItems.ForEach(x =>
            {
                if (x.Product.Quantity > x.Count)
                {
                    Log.Information($"cart items quantity :{x.Count} is more than product quantity: {x.Product.Quantity}");
                    isValid = false;
                }
            });

            Log.Information($"cart ValidateQunatity is: {isValid}");
            return isValid;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }
    private bool ValidateTotalCost(ShoppingCart cart)
    {
        try
        {
            Log.Information("ValidateTotalCost at ShoppingCartServices.cs");
            var totalCost = cart.TotalCost;
            cart.ShoppingCartItems.ForEach(x =>
            {
                x.TotalCost = x.Product.Price * x.Count;
            });

            var newTotalCost = cart.ShoppingCartItems.Sum(x => x.TotalCost);

            if (cart.TotalCost != newTotalCost)
            {
                Log.Information($"totalCost is not valid - cartTotalCost: {cart.TotalCost} - newTotalCost: {newTotalCost}");
                return false;
            }

            Log.Information($"totalCost is valid - value:{newTotalCost}");
            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }

    public DetailedShoppingCartDto GetShoppingCart(int id)
    {
        try
        {
            Log.Information($"request GetShoppingCart id = {id}");
            var cart = _context.ShoppingCarts.ProjectToType<DetailedShoppingCartDto>().FirstOrDefault(x => x.Id == id);
            if (cart == null)
            {
                Log.Information($"shoppingCart with id = {id} not founded");
                return null;
            }
            Log.Information($"return shoppingCart with id = {id}");
            return cart;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
    public List<DetailedShoppingCartDto> GetShoppingCarts()
    {
        try
        {
            Log.Information($"request GetShoppingCarts");
            var carts = _context.ShoppingCarts.ProjectToType<DetailedShoppingCartDto>().ToList();
            if (carts != null)
            {
                Log.Information($"shoppingCarts returned Count = {carts.Count}");
                return carts;
            }
            return null;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
    public void DeleteShoppingCart(int id)
    {
        try
        {
            Log.Information($"request DeleteShoppingCart to delete id = {id}");
            var cart = _context.ShoppingCarts.Find(id);
            if (cart != null)
            {

                _context.ShoppingCarts.Remove(cart);
                _context.SaveChanges();
                Log.Information($"shoppingCart was deleted id = {id}");
            }
            Log.Information($"shoppingCart not founded id = {id}");
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
}
