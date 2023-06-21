using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DB;
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
	public int AddShoppingCart(ShoppingCartDto item)
	{
		try
		{
            Log.Information($"request AddShoppingCart to add item data = {item}");
            var cart = _mapper.Map<ShoppingCart>(item);
			_context.ShoppingCarts.Add(cart);
			_context.SaveChanges();
            Log.Information($"cart was added with id  = {cart.Id}");
            return cart.Id;

		}
		catch (Exception ex)
		{

            Log.Error(ex.Message, ex);
            return -1;
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


	public void EditShoppingCart(EditShoppingCartItemDto shoppingCartDto)
	{


		// TODO: Global Exception Handling
		try
		{
            Log.Information($"request EditShoppingCart with data = {shoppingCartDto}");
            var cart = _mapper.Map<ShoppingCart>(shoppingCartDto);
			if (cart != null)
			{
				_context.ShoppingCarts.Update(cart);
				_context.SaveChanges();
                Log.Information($"shopping cart was updated with data = {shoppingCartDto}");
            }
		}
		catch (Exception ex)
		{
            Log.Error(ex.ToString());
        }
	}

	public void EditShoppingCart(DetailedShoppingCartDto shoppingCartDto)
	{
		try
		{
			var cart = _mapper.Map<ShoppingCart>(shoppingCartDto);

			var cartDb = _context.ShoppingCarts.Include(x => x.ShoppingCartItems).FirstOrDefault(x => x.Id == shoppingCartDto.Id);

			if (cartDb == null)
			{
				_context.Add(cart);
			}
			else
			{
				_context.Entry(cartDb).CurrentValues.SetValues(cart);


				// TODO: update the logic to optimize the performance 
				foreach (var item in cart.ShoppingCartItems)
				{
					var existingCartItem = cartDb.ShoppingCartItems
						.FirstOrDefault(x => x.Id == item.Id);

					if (existingCartItem == null)
					{
						_context.Add(item);
					}
					else
					{
						existingCartItem.TotalCost = item.TotalCost;
					}
				}
				foreach (var item in cartDb.ShoppingCartItems)
				{
					if (!cart.ShoppingCartItems.Any(x => x.Id == item.Id))
					{
						_context.Remove(item);
					}
				}
			}
			_context.SaveChanges();
		}
		catch (Exception ex)
		{
            Log.Error(ex.ToString());
        }
	}

	public DetailedShoppingCartDto GetShoppingCart(int id)
	{
		try
		{
            Log.Information($"request GetShoppingCart id = {id}");
            var cart = _mapper.ProjectTo<DetailedShoppingCartDto>(_context.ShoppingCarts).FirstOrDefault(x => x.Id == id);
			if(cart == null)
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
            var carts =  _mapper.ProjectTo<DetailedShoppingCartDto>(_context.ShoppingCarts).ToList();
			if(carts != null)
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

}
