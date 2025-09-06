namespace Kakhnouski_153503.Domain.Entities;

public class Author : Entity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int? Age { get; set; }
    public string? Email { get; set; }
    public string? GenresOfBooks { get; set; }
    public List<Book>? Books { get; set;}
}
