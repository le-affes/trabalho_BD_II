namespace AppDelivery.Model;

public class Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}