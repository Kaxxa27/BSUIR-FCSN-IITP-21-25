namespace TaxiApplication.WEB.Controllers;

public class AccountController : Controller
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	#region HttpGet
	[HttpGet]
	public IActionResult Login() => View();

	[HttpGet]
	public IActionResult Registration() => View();
	#endregion

	#region HttpPost
	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel loginViewModel)
	{
		if (ModelState.IsValid)
		{
			var response = await _accountService.LoginAsync(loginViewModel);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				await HttpContext.SignInAsync(response.Data!);
				return RedirectToAction("Index", "Home");
			}
			ModelState.AddModelError("", response.Description);
		}

		return View(loginViewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Registration(Client client) 
	{
		if (ModelState.IsValid)
		{
			var response = await _accountService.RegistrationAsync(client);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				await HttpContext.SignInAsync(response.Data!);
				return RedirectToAction("Index", "Home");
			}
			ModelState.AddModelError("", response.Description);
		}

		return View(client);
	}
	#endregion


	[HttpGet]
	public IActionResult AccessDenied() => StatusCode(403, "Forbidden");

	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync();
		return RedirectToAction("Index", "Home");
	}
}
