namespace Kakhnouski_153503.Domain.Entities;

public class Book : Entity
{
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public double? Price { get; set; }
    public double? Rating { get; set; }
    public DateTime? DateOfWriting { get; set; }
    public int? AuthorID { get; set; }  
}
