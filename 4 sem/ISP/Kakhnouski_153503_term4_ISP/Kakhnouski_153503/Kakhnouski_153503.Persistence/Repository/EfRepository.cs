using System.Security.Cryptography.X509Certificates;

namespace Kakhnouski_153503.Persistence.Repository;

public class EfRepository<T> : IRepository<T> where T : Entity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _entities;
    public EfRepository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity);
    }

    public  async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        if(_entities.Contains(entity) is true)
           _entities.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<T> FirstOrDefaultAsync(Func<T, bool>? filter = null, CancellationToken cancellationToken = default)
    {
        if (filter is null)
            return await _entities.FirstOrDefaultAsync() ?? throw new ArgumentNullException();
        return _entities.Where(filter).FirstOrDefault() ?? throw new ArgumentNullException();
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _entities.ToList().Find(x => x.Id == id)) ?? throw new ArgumentNullException();
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await _entities.ToListAsync() ?? throw new ArgumentNullException();
    }

    public async Task<IReadOnlyList<T>> ListAsync(Func<T, bool> filter, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _entities.Where(filter).ToList());
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }
}

