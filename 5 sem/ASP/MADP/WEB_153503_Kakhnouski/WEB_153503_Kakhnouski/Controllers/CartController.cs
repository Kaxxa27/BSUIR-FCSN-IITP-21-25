using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_153503_Kakhnouski.Domain.Models.Cart;
using WEB_153503_Kakhnouski.Extensions;
using WEB_153503_Kakhnouski.Services.CarService;

namespace WEB_153503_Kakhnouski.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICarService _carService;
    private readonly Cart _cart;

    public CartController(ICarService carService, Cart cart)
    {
        _carService = carService;
        _cart = cart;
    }

    public IActionResult Index()
    {
        return View(_cart.CartItems);
    }

    public async Task<IActionResult> Add(int id, string returnUrl)
    {
        var response = await _carService.GetCarByIdAsync(id);
        if (response.Success)
        {
            _cart.AddToCart(response.Data!);
        }

        return Redirect(returnUrl);
    }

    public IActionResult Remove(int id, string returnUrl)
    {
        _cart.Remove(id);

        return Redirect(returnUrl);
    }
}
