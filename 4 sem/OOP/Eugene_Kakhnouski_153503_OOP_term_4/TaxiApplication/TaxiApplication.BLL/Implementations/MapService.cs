using BingMapsRESTToolkit;

namespace TaxiApplication.BLL.Implementations;

public class MapService : IMapService
{
	// BingMapsKey
	private readonly string BING_MAPS_KEY = "AovVDz7evAPwd8iLhM6pBwRHM3wJD5Z2OJHpYwFeRktRbw5wF5-wYkaf9JycZGHW";
	public async Task<IBaseResponse<TaxiOrderViewModel>>  CollectInfoFromRequest(RouteRequest request, TaxiOrderViewModel taxiOrderViewModel)
	{
		//BingMapsRESTToolkit
		try
		{
			var response = await ServiceManager.GetResponseAsync(request);
			var result = response.ResourceSets[0]?.Resources[0] as BingMapsRESTToolkit.Route;

			taxiOrderViewModel.Route.Time = TimeSpan.FromSeconds(result!.RouteLegs[0].TravelDuration);
			taxiOrderViewModel.Route.Distance = result.RouteLegs[0].TravelDistance;

			// Пофиксить, убрать ViewModel и сделать работу только с Model, но пока что так XD
			TaxiOrder testTaxiOrder = taxiOrderViewModel.Order;
			testTaxiOrder.CurrentRoute = taxiOrderViewModel.Route;

			return new BaseResponse<TaxiOrderViewModel>()
			{
				Data = taxiOrderViewModel,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[MapService.CollectInfoFromRequest] error: {ex.Message})");
			return new BaseResponse<TaxiOrderViewModel>()
			{
				Data = null,
				StatusCode = StatusCode.AllError
			};
		}
	}
	public async Task<IBaseResponse<RouteRequest>> CreateRouteRequest(TaxiOrderViewModel taxiOrderViewModel)
	{
		try
		{
			// Создания запроса к REST API для вычисления маршрута между точками
			var request = new RouteRequest
			{
				BingMapsKey = BING_MAPS_KEY,
				RouteOptions = new BingMapsRESTToolkit.RouteOptions()
				{
					TravelMode = TravelModeType.Driving,
					DistanceUnits = DistanceUnitType.Kilometers
				},
				Waypoints = new List<SimpleWaypoint>
				{
					new SimpleWaypoint(taxiOrderViewModel.Route.StartLocation),
					new SimpleWaypoint(taxiOrderViewModel.Route.EndLocation)
				}
			};

			return new BaseResponse<RouteRequest>()
			{
				Data = request,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[MapService.CreateRouteRequest] error: {ex.Message})");
			return new BaseResponse<RouteRequest>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}
}
