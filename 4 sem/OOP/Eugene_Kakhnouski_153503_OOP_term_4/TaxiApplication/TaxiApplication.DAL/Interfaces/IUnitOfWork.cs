namespace TaxiApplication.DAL.Interfaces;
public interface IUnitOfWork
{
	IRepository<User> UserRepository { get; }
	IRepository<Client> ClientRepository { get; }
	IRepository<ClientProfile> ClientProfileRepository { get; }
	IRepository<TaxiOrder> TaxiOrderRepository { get; }
	IRepository<Route> RouteRepository { get; }

	//IRepository<Driver> DriverRepository { get; }
	public Task RemoveDatbaseAsync();
	public Task CreateDatabaseAsync();
	public Task SaveAllAsync();
}

