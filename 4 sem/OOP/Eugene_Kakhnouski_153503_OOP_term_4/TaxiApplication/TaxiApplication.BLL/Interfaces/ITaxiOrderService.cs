using TaxiApplication.Domain.Entity;

namespace TaxiApplication.BLL.Interfaces;

public interface ITaxiOrderService
{
	Task<IBaseResponse<TaxiOrder>> GetTaxiOrder(int id);
	Task<IBaseResponse<IEnumerable<TaxiOrder>>> GetAllTaxiOrders();
	Task<IBaseResponse<IEnumerable<TaxiOrder>>> GetAllClientTaxiOrders(int id);
	Task<IBaseResponse<TaxiOrder>> AddTaxiOrder(TaxiOrder taxiOrder);
	Task<IBaseResponse<bool>> DeleteTaxiOrder(int id);
	Task<IBaseResponse<TaxiOrder>> FirstOrDefault(Func<TaxiOrder, bool> filter);
	Task<IBaseResponse<bool>> UpdateTaxiOrder(TaxiOrder taxiOrder);
	Task<IBaseResponse<double>> CalculatePrice(TaxiOrder taxiOrder);


}
