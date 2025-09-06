using BingMapsRESTToolkit;

namespace TaxiApplication.BLL.Interfaces;

public interface IMapService
{
	public Task<IBaseResponse<RouteRequest>> CreateRouteRequest(TaxiOrderViewModel taxiOrderViewModel);
	public Task<IBaseResponse<TaxiOrderViewModel>> CollectInfoFromRequest(RouteRequest request, TaxiOrderViewModel taxiOrderViewModel);
}
