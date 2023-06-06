using Domain.Entities;

namespace Application.Dtos.ShoppingCartItemDto
{
    public class DetailedShoppingCartItemDto
    {
        public int Id { get; set; }
        public decimal TotalCost { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
