using Application.Contracts;
using Application.Dtos.ShoppingCartItem;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DB;
using Serilog;

namespace Infrastructure.Services.ShoppingCartItemItemServices
{
    public class ShoppingCartItemServices : IShoppingCartItemServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartItemServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddListShoppingCartItems(int shoppingCartId, List<ShoppingCartItemDto> items)
        {
            Log.Information($"request to add item id = {shoppingCartId} and item = {items}");
            try
            {
                var cart = _context.ShoppingCarts.Find(shoppingCartId);
                if (cart != null)
                {
                    var shoppingCartitems = _mapper.Map<List<ShoppingCartItem>>(items).ToList();
                    shoppingCartitems.ForEach(item => item.ShoppingCartId = shoppingCartId);

                    _context.ShoppingCartItems.AddRange(shoppingCartitems);
                    _context.SaveChanges();
                    Log.Information($"item was add with id = {shoppingCartId} and item = {items}");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return false;
            }
        }

        public int AddShoppingCartItem(ShoppingCartItemDto item)
        {
            try
            {
                Log.Information($"request to add item data = {item}");
                var CartItem = _mapper.Map<ShoppingCartItem>(item);
                _context.ShoppingCartItems.Add(CartItem);
                _context.SaveChanges();
                Log.Information($"item was added item data = {item}");
                return CartItem.Id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return 0;
            }
        }

        public void DeleteShoppingCartItem(int id)
        {
            try
            {
                Log.Information($"request to delete item id = {id}");
                var CartItem = _context.ShoppingCartItems.Find(id);
                if (CartItem != null)
                {
                    _context.ShoppingCartItems.Remove(CartItem);
                    _context.SaveChanges();
                    Log.Information($"item was deleted id = {id}");
                }
                Log.Information($" id = {id} not founded");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                
            }
        }

        public void EditShoppingCartItem(UpdateShoppingCartItemDto shoppingCartItemDto)
        {
            try
            {
                Log.Information($"request to update item data = {shoppingCartItemDto}");
                var CartItem = _mapper.Map<ShoppingCartItem>(shoppingCartItemDto);
                if (CartItem != null)
                {
                    _context.ShoppingCartItems.Update(CartItem);
                    _context.SaveChanges();
                    Log.Information($"item was updated id = {shoppingCartItemDto.Id}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }
        }
        public DetailedShoppingCartItemDto GetShoppingCartItem(int id)
        {
            try
            {
                Log.Information($"request to get item with id = {id}");
                var item = _mapper.ProjectTo<DetailedShoppingCartItemDto>(_context.ShoppingCartItems).FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    Log.Information($"item returned with id = {id}");
                    return item;
                }
                Log.Information($"item with id = {id} not founded");
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }
        public List<DetailedShoppingCartItemDto> GetShoppingCartItems()
        {
            try
            {
                Log.Information($"request to get shoppingCart items");
                var items = _mapper.ProjectTo<DetailedShoppingCartItemDto>(_context.ShoppingCartItems).ToList();

                if( items != null)
                {
                    Log.Information($"returned shoppingCartItems list count {items.Count}");
                    return items;
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

    }
}
