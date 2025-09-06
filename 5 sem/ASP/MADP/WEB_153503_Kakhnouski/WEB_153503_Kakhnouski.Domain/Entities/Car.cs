namespace WEB_153503_Kakhnouski.Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public double Price { get; set; }
    public Category? Category { get; set; }
    public string? Image { get; set; }
}
