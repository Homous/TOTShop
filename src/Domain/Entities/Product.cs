using System.ComponentModel.DataAnnotations;


namespace Domain.Entities;

public class Product : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    [MaxLength(255)]
    public string Description { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    [MaxLength(255)]
    public string ImageUrl { get; set; }
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

    public override string ToString()
    {
        return $"Id: {Id},  Name: {Name}, Price: {Price}, Description: {Description}, Quantity: {Quantity}, ImageUrl: {ImageUrl}";
    }
}
