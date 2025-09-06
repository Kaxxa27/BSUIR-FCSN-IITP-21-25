using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEB_153503_Kakhnouski.Controllers;

public class IdentityController : Controller
{
    public async Task Login()
    {
        await HttpContext.ChallengeAsync(
            "oidc",
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            }
        );
    }

    [HttpPost]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync("cookie");
        await HttpContext.SignOutAsync("oidc",
            new AuthenticationProperties
            {
                RedirectUri= Url.Action("Index", "Home")
            });
    }
}
