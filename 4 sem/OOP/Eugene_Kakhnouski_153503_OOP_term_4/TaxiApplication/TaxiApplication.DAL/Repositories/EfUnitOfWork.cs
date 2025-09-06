

namespace TaxiApplication.DAL.Repositories;

public class EfUnitOfWork : IUnitOfWork
{
	private readonly TaxiApplicationDbContext _context;
	private readonly IRepository<Client> _clientRepository;
	private readonly IRepository<User> _userRepository;
	private readonly IRepository<ClientProfile> _clientProfileRepository;
	private readonly IRepository<TaxiOrder> _taxiOrderRepository;
	private readonly IRepository<Route> _routeRepository;

	public EfUnitOfWork(TaxiApplicationDbContext context)
	{
		_context = context;
		_clientRepository = new EfRepository<Client>(context);
		_userRepository = new EfRepository<User>(context);
		_clientProfileRepository = new EfRepository<ClientProfile>(context);
		_taxiOrderRepository = new EfRepository<TaxiOrder>(context);
		_routeRepository = new EfRepository<Route>(context);
	}

	IRepository<Client> IUnitOfWork.ClientRepository => _clientRepository;
	IRepository<User> IUnitOfWork.UserRepository => _userRepository;
	IRepository<ClientProfile> IUnitOfWork.ClientProfileRepository => _clientProfileRepository;
	IRepository<TaxiOrder> IUnitOfWork.TaxiOrderRepository => _taxiOrderRepository;
	IRepository<Route> IUnitOfWork.RouteRepository => _routeRepository;

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
