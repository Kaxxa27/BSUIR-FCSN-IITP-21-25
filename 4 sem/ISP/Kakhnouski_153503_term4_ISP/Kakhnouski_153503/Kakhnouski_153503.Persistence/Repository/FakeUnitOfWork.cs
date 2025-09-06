using SQLitePCL;

namespace Kakhnouski_153503.Persistence.Repository;

public class FakeUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Lazy<IRepository<Author>> _authorRepository;
    public IRepository<Author> AuthorRepository => _authorRepository.Value;

    public FakeUnitOfWork(AppDbContext appDbContext)
    {
        _context = appDbContext;
        _authorRepository = new Lazy<IRepository<Author>>(() => new EfRepository<Author>(_context));
    }
    public IRepository<Book> BookRepository => throw new NotImplementedException();

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
