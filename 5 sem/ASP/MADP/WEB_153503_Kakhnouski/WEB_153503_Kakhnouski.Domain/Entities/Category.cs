namespace WEB_153503_Kakhnouski.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? NormalizedName { get; set; }
}
