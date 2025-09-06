

namespace TaxiApplication.WEB.Areas.Map.Controllers;

[Area("Client")]
public class ClientController : Controller
{
	ITaxiOrderService _taxiOrderService { get; set; }
	IMapService _mapService { get; set; }
	IClientService _clientService { get; set; }
	public ClientController(ITaxiOrderService taxiOrderService, IMapService mapService, IClientService clientService)
	{
		_taxiOrderService = taxiOrderService;
		_mapService = mapService;
		_clientService = clientService;
	}

	private bool CheckUserStatus()
	{
		int ID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

		var All_Clients = (_clientService.GetAllClients()).Result.Data;
		var client = All_Clients!.FirstOrDefault(c => c.Id == ID);

		return client is null ? true : false;
	}


	[HttpGet]
	public async Task<IActionResult> ProfileEdit()
	{
		if (CheckUserStatus() is true) return RedirectToAction("Logout", "Account", new { area = "" });
		var client = (await _clientService.GetClient(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!))).Data;
		return View(client);
	}

	[HttpPost]
	public async Task<IActionResult> ProfileEdit(Client client)
	{
		if (CheckUserStatus() is true) return RedirectToAction("Logout", "Account", new { area = "" });

		client.Profile.Photo = (await _clientService.GetClient(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!))).Data!.Profile.Photo;
		var response = await _clientService.UpdateClient(client);

		if (response.StatusCode == Domain.Enum.StatusCode.OK)
		{
			return View(client);
		}
		return View(client/*Страница ошибки*/);
	}

	[HttpPost]
	public async Task<IActionResult> UploadPhoto(IFormFile photoFile)
	{
		if (CheckUserStatus() is true) return RedirectToAction("Logout", "Account", new { area = "" });
		try
		{
			if (photoFile != null && photoFile.Length > 0)
			{
				byte[] photoBytes;
				using (var memoryStream = new MemoryStream())
				{
					await photoFile.CopyToAsync(memoryStream);
					photoBytes = memoryStream.ToArray();
				}
				var client = (await _clientService.GetClient(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!))).Data;
				client!.Profile.Photo = photoBytes;
				await _clientService.UpdateClient(client);
			}

			return RedirectToAction("ProfileEdit");
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[ClientController.UploadPhoto] error: {ex.Message})");
			return RedirectToAction("ProfileEdit");
		}
	}


	[HttpPost]
	public async Task<IActionResult> DeleteProfile(Client client)
	{
		if (CheckUserStatus() is true) return RedirectToAction("Logout", "Account", new { area = "" });
		try
		{
			await _clientService.DeleteClient(client.Id);
			return RedirectToAction("Logout", "Account", new { area = "" });
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[ClientController.DeleteProfile] error: {ex.Message})");
			return RedirectToAction("Logout", "Account", new { area = "" });
		}
	}


	[HttpGet]
	public IActionResult CreateTaxiOrder()
	{
		if (CheckUserStatus() is true) return RedirectToAction("Logout", "Account", new { area = "" });
		return View();
	}


	[HttpPost]
	public async Task<IActionResult> CreateTaxiOrder(TaxiOrderViewModel taxiOrderViewModel)
	{
		if (CheckUserStatus() is true) return RedirectToAction("Logout", "Account", new { area = "" });
		try
		{
			var request = (await _mapService.CreateRouteRequest(taxiOrderViewModel)).Data;

			taxiOrderViewModel = (await _mapService.CollectInfoFromRequest(request, taxiOrderViewModel)).Data!;

			
			// Проверка на корректность заказа
			if (taxiOrderViewModel.Route.Distance == 0 && taxiOrderViewModel.Route.Time.TotalSeconds == 0)
			{
				return RedirectToAction("CreateTaxiOrder");
			}

			taxiOrderViewModel.Order.Price = (await _taxiOrderService.CalculatePrice(taxiOrderViewModel.Order)).Data;
			var json = JsonSerializer.Serialize(taxiOrderViewModel);

			TempData["taxiOrderViewModel_JSON"] = json;
			return RedirectToAction(actionName: "Index", controllerName: "Map", new { area = "Map" });		
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[ClientController.CreateTaxiOrder] error: {ex.Message})");
			return RedirectToAction("CreateTaxiOrder");
		}
	}

	[HttpGet]
	public IActionResult GetTaxiOrders()
	{
		if (CheckUserStatus() is true) return RedirectToAction("Logout", "Account", new { area = "" });
		return View(_taxiOrderService.GetAllClientTaxiOrders(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)).Result.Data);
	}

	[HttpPost]
	public async Task<IActionResult> SaveTaxiOrder(string taxiOrderViewModel)
	{
		if (CheckUserStatus() is true) return RedirectToAction("Logout", "Account", new { area = "" });
		try
		{
			// Десериализация
			var deserializedObject = JsonSerializer.Deserialize<TaxiOrderViewModel>(taxiOrderViewModel);

			TaxiOrder taxiOrder = deserializedObject!.Order;
			taxiOrder.CurrentRoute = deserializedObject.Route;
			taxiOrder.ClientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

			await _taxiOrderService.AddTaxiOrder(taxiOrder);

			return RedirectToAction("Index", "Home", new { area = "" });
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[ClientController.SaveTaxiOrder] error: {ex.Message})");
			return RedirectToAction("CreateTaxiOrder");
		}
	}
}
