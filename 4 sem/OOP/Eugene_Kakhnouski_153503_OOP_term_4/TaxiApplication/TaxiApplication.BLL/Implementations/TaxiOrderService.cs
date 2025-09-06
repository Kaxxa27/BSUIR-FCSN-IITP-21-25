using System.Formats.Tar;
using TaxiApplication.Domain.Enum;

namespace TaxiApplication.BLL.Implementations;

public class TaxiOrderService : ITaxiOrderService
{
	private readonly IUnitOfWork _unitOfWork;

	// Цена за километр в рублях.
	private readonly double FixedPrice = 1;
	public TaxiOrderService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IBaseResponse<TaxiOrder>> AddTaxiOrder(TaxiOrder taxiOrder)
	{
		try
		{
			await _unitOfWork.TaxiOrderRepository.AddAsync(taxiOrder);

			return new BaseResponse<TaxiOrder>()
			{
				Data = taxiOrder,
				Description = "Заказ добавлен.",
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[TaxiOrderService.AddTaxiOrder] error: {ex.Message})");
			return new BaseResponse<TaxiOrder>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}


	}

	public async Task<IBaseResponse<bool>> DeleteTaxiOrder(int id)
	{
		try
		{
			var realOrder = await _unitOfWork.TaxiOrderRepository.FirstOrDefaultAsync(x => x.Id == id);
			if (realOrder is null)
			{
				return new BaseResponse<bool>()
				{
					Data = false,
					Description = "Заказ не найден.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			await _unitOfWork.TaxiOrderRepository.DeleteAsync(realOrder);

			return new BaseResponse<bool>()
			{
				Data = true,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[TaxiOrderService.DeleteClient] error: {ex.Message})");
			return new BaseResponse<bool>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

	public async Task<IBaseResponse<TaxiOrder>> FirstOrDefault(Func<TaxiOrder, bool> filter)
	{
		try
		{
			var realOrder = await _unitOfWork.TaxiOrderRepository.FirstOrDefaultAsync(filter);
			if (realOrder is null)
			{
				return new BaseResponse<TaxiOrder>()
				{
					Description = "Заказ не найден.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			var route = await _unitOfWork.RouteRepository.FirstOrDefaultAsync(cl => cl.TaxiOrderId == realOrder.Id);

			if (route == null)
			{
				return new BaseResponse<TaxiOrder>()
				{
					StatusCode = StatusCode.AllError,
					Description = $"Заказ с таким маршрутом не найден."
				};
			}
			realOrder.CurrentRoute = route;


			return new BaseResponse<TaxiOrder>()
			{
				Data = realOrder,
				Description = "Заказ найден.",
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[TaxiOrdertService.FirstOrDefault] error: {ex.Message})");
			return new BaseResponse<TaxiOrder>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};

		}
	}

	public async Task<IBaseResponse<IEnumerable<TaxiOrder>>> GetAllTaxiOrders()
	{
		try
		{
			var AllOrders = await _unitOfWork.TaxiOrderRepository.ListAllAsync();

			if (AllOrders is null)
			{
				return new BaseResponse<IEnumerable<TaxiOrder>>()
				{
					Description = "Заказы отсутсвуют.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			foreach (var order in AllOrders)
			{
				var route = await _unitOfWork.RouteRepository.FirstOrDefaultAsync(cl => cl.TaxiOrderId == order.Id);

				if (route == null)
				{
					return new BaseResponse<IEnumerable<TaxiOrder>>()
					{
						StatusCode = StatusCode.AllError,
						Description = $"Заказ с таким маршрутом не найден."
					};
				}
				order.CurrentRoute = route;
			}

			return new BaseResponse<IEnumerable<TaxiOrder>>()
			{
				Data = AllOrders,
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[TaxiOrderService.GetAllClients] error: {ex.Message})");
			return new BaseResponse<IEnumerable<TaxiOrder>>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

	public async Task<IBaseResponse<TaxiOrder>> GetTaxiOrder(int id)
	{
		try
		{
			var realOrder = await _unitOfWork.TaxiOrderRepository.FirstOrDefaultAsync(x => x.Id == id);
			var route = await _unitOfWork.RouteRepository.FirstOrDefaultAsync(x => x.TaxiOrderId == id);

			if (realOrder is null || route is null)
			{
				return new BaseResponse<TaxiOrder>()
				{
					Description = "Заказ отсутсвует.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			realOrder.CurrentRoute = route;
			return new BaseResponse<TaxiOrder>()
			{
				Data = realOrder,
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[TaxiOrderService.GetOrder] error: {ex.Message})");
			return new BaseResponse<TaxiOrder>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

	public async Task<IBaseResponse<bool>> UpdateTaxiOrder(TaxiOrder taxiOrder)
	{
		try
		{
			var realOrder = await _unitOfWork.TaxiOrderRepository.FirstOrDefaultAsync(x => x.Id == taxiOrder.Id);
			if (realOrder is null)
			{
				return new BaseResponse<bool>()
				{
					Description = "Заказ отсутсвует.",
					StatusCode = StatusCode.UserNotFound
				};
			}
			realOrder.CurrentRoute = taxiOrder.CurrentRoute;

			await _unitOfWork.TaxiOrderRepository.UpdateAsync(realOrder);

			return new BaseResponse<bool>()
			{
				Data = true,
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[TaxiOrderService.UpdateOrder] error: {ex.Message})");
			return new BaseResponse<bool>()
			{
				Data = false,
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}


	public async Task<IBaseResponse<IEnumerable<TaxiOrder>>> GetAllClientTaxiOrders(int id)
	{
		try
		{
			var AllClientOrders = (await _unitOfWork.TaxiOrderRepository.ListAllAsync())
												.Where(x => x.ClientId == id)
												.ToList();

			if (AllClientOrders is null)
			{
				return new BaseResponse<IEnumerable<TaxiOrder>>()
				{
					Description = "Заказы отсутсвуют.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			foreach (var order in AllClientOrders)
			{
				var route = await _unitOfWork.RouteRepository.FirstOrDefaultAsync(cl => cl.TaxiOrderId == order.Id);

				if (route == null)
				{
					return new BaseResponse<IEnumerable<TaxiOrder>>()
					{
						StatusCode = StatusCode.AllError,
						Description = $"Заказ с таким маршрутом не найден."
					};
				}
				order.CurrentRoute = route;
			}

			return new BaseResponse<IEnumerable<TaxiOrder>>()
			{
				Data = AllClientOrders,
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[TaxiOrderService.GetAllClientTaxiOrders] error: {ex.Message})");
			return new BaseResponse<IEnumerable<TaxiOrder>>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

	public async Task<IBaseResponse<double>> CalculatePrice(TaxiOrder taxiOrder)
	{
		try
		{
			double tariff = taxiOrder.Tariff switch
			{
				Tariff.Economy => 1.1,
				Tariff.Comfort => 1.3,
				Tariff.Business => 1.7,
				Tariff.VIP => 2,
				_ => throw new NotImplementedException()
			};

			return new BaseResponse<double>()
			{
				Data = Math.Round(FixedPrice * taxiOrder.CurrentRoute.Distance * tariff, 2),
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[TaxiOrderService.CalculatePrice] error: {ex.Message})");
			return new BaseResponse<double>()
			{
				Data = 0,
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

}
