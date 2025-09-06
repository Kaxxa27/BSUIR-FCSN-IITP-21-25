using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models.Cart;
using WEB_153503_Kakhnouski.Extensions;

namespace WEB_153503_Kakhnouski.Services.CartService;

public class SessionCart : Cart
{
    private readonly ISession _session;
    public SessionCart(IHttpContextAccessor httpContextAccessor)
    {
        _session = httpContextAccessor.HttpContext!.Session;

        // Cart stored in session.
        Cart? sessionCart = _session.Get<Cart>(nameof(Cart));
        

        if (sessionCart is not null)
            base.CartItems = sessionCart.CartItems;
    }

    public override void AddToCart(Car car)
    {
        base.AddToCart(car);
        _session.Set<Cart>(nameof(Cart), this);
    }

    public override void Remove(int id)
    {
        base.Remove(id);
        _session.Set(nameof(Cart), this);
    }

    public override void ClearAll()
    {
        base.ClearAll();
        _session.Remove(nameof(Cart));
    }
}