namespace Kakhnouski_153503.Persistence.Repository;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Lazy<IRepository<Author>> _authorRepository;
    private readonly Lazy<IRepository<Book>> _bookRepository;
    public IRepository<Author> AuthorRepository => _authorRepository.Value;
    public IRepository<Book> BookRepository => _bookRepository.Value;

    public EfUnitOfWork(AppDbContext context)
    {
        _context = context;

        _authorRepository = new Lazy<IRepository<Author>>(() =>
        new EfRepository<Author>(context));

        _bookRepository = new Lazy<IRepository<Book>>(() =>
        new EfRepository<Book>(context));
    }

    public async Task CreateDatabaseAsync()
    {
        await _context.Database.EnsureCreatedAsync();
    }
    public async Task RemoveDatbaseAsync()
    {
        await _context.Database.EnsureDeletedAsync();
    }
    public async Task SaveAllAsync()
    {
        await _context.SaveChangesAsync();
    }

}
