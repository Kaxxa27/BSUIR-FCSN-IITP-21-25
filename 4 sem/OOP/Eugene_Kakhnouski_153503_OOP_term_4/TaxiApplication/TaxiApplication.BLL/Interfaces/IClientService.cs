namespace TaxiApplication.BLL.Interfaces;

public interface IClientService
{
	Task<IBaseResponse<Client>> GetClient(int id);
	Task<IBaseResponse<IEnumerable<Client>>> GetAllClients();
	Task<IBaseResponse<Client>> AddClient(Client Client);
	Task<IBaseResponse<bool>> DeleteClient(int id);
	Task<IBaseResponse<Client>> FirstOrDefault(Func<Client, bool> filter);
    Task<IBaseResponse<bool>> UpdateClient(Client Client);
}
