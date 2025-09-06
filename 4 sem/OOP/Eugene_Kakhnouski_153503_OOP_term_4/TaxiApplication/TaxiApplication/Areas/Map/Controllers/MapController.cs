namespace TaxiApplication.WEB.Areas.Map.Controllers;

[Area("Map")]
public class MapController : Controller
{
	public IActionResult Index()
	{
		try
		{
			// Десериализация TaxiViewModel
			var deserializedViewModel = JsonSerializer.Deserialize<TaxiOrderViewModel>((string)TempData["taxiOrderViewModel_JSON"]!);

			//TaxiOrderViewModel viewModel = deserializedObject! as TaxiOrderViewModel
			TaxiOrderViewModel viewModel = deserializedViewModel!;
			if (viewModel is null) return View();
			return View(viewModel);
		}
		catch (Exception)
		{
			return View();
		}
	}
}
