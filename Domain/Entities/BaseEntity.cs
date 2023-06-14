namespace Domain.Entities;

// Base Entity
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.Today;
}
