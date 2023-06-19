using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

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
			var cart = _mapper.Map<ShoppingCart>(item);

			_context.ShoppingCarts.Add(cart);
			_context.SaveChanges();

			return cart.Id;

		}
		catch (Exception ex)
		{


			// TODO: Use SiriLOG
			Console.WriteLine($"AddingShoppingCart has failed {ex.Message}");
			Console.WriteLine(ex);
			return -1;
		}
	}

	public void DeleteShoppingCart(int id)
	{
		try
		{
			var cart = _context.ShoppingCarts.Find(id);
			_context.ShoppingCarts.Remove(cart);
			_context.SaveChanges();

		}
		catch (Exception ex)
		{
			Console.WriteLine($"DeleteShoppingCart has failed {ex.Message}");
			Console.WriteLine(ex);
		}
	}


	public void EditShoppingCart(EditShoppingCartItemDto shoppingCartDto)
	{


		// TODO: Global Exception Handling
		try
		{
			var cart = _mapper.Map<ShoppingCart>(shoppingCartDto);
			if (cart != null)
			{
				_context.ShoppingCarts.Update(cart);
				_context.SaveChanges();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"EditShoppingCart has failed {ex.Message}");
			Console.WriteLine(ex);
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
			Console.WriteLine($"EditShoppingCart has failed {ex.Message}");
			Console.WriteLine(ex);
			throw ex;
		}
	}

	public DetailedShoppingCartDto GetShoppingCart(int id)
	{
		try
		{
			return _mapper.ProjectTo<DetailedShoppingCartDto>(_context.ShoppingCarts).FirstOrDefault(x => x.Id == id);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"GetShoppingCart has failed {ex.Message}");
			Console.WriteLine(ex);
			return null;
		}
	}

	public List<DetailedShoppingCartDto> GetShoppingCarts()
	{
		try
		{
			return _mapper.ProjectTo<DetailedShoppingCartDto>(_context.ShoppingCarts).ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"GetShoppingCarts has failed {ex.Message}");
			Console.WriteLine(ex);
			return null;
		}
	}

}
