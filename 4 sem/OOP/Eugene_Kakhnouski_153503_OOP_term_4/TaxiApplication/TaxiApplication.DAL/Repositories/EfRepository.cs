namespace TaxiApplication.DAL.Repositories;

public class EfRepository<T> : IRepository<T> where T : Entity
{
	protected readonly TaxiApplicationDbContext _context;
	protected readonly DbSet<T> _entities;
	public EfRepository(TaxiApplicationDbContext context)
	{
		_context = context;
		_entities = context.Set<T>();
	}

	public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
	{
		await _entities.AddAsync(entity);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
	{
		await Task.Run(()=> _entities.Remove(entity));
		await _context.SaveChangesAsync();
	}

	public async Task<T?> FirstOrDefaultAsync(Func<T, bool> filter, CancellationToken cancellationToken = default)
	{
		return await Task.Run(() => _entities.FirstOrDefault(filter));
	}

	public async Task<T?> GetByIdAsync(int id)
	{
		return await Task.Run(() => _entities.FirstOrDefault(x => x.Id == id));
	}

	public async Task<IEnumerable<T>> ListAllAsync(CancellationToken cancellationToken = default)
	{
		return await _entities.ToListAsync(); 
	}

	public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
	{
		await Task.Run(() => _entities.Update(entity));
		await _context.SaveChangesAsync();
	}
}
