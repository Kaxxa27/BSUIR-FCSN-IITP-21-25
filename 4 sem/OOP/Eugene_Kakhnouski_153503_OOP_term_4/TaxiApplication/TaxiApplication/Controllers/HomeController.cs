namespace TaxiApplication.Controllers;

public class HomeController : Controller
{

	IUnitOfWork _repository;
	public HomeController(IUnitOfWork repository)
	{
		_repository = repository;
	}
	public IActionResult Index() => View();

	[HttpGet]
	public IActionResult AddItem() => View();
}
