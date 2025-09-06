namespace Kakhnouski_153503.Domain.Abstractions;

public interface IUnitOfWork
{
    IRepository<Author> AuthorRepository { get; }
    IRepository<Book> BookRepository { get; }
    public Task RemoveDatbaseAsync();
    public Task CreateDatabaseAsync();
    public Task SaveAllAsync();
}
