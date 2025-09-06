using WEB_153503_Kakhnouski.Extensions;
using WEB_153503_Kakhnouski.Models;
using Microsoft.AspNetCore.Mvc;
using WEB_153503_Kakhnouski.Domain.Models.Cart;

namespace WEB_153503_Kakhnouski.Components;

public class CartViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _contextAccessor;

    private readonly Cart _cart;
    public CartViewComponent(Cart cart, IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
        _cart = cart;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("Cart", _cart);
    }
}
