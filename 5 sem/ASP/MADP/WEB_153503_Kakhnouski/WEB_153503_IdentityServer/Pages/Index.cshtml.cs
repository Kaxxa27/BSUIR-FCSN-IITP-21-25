using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace WEB_153503_IdentityServer.Pages.Home
{
    [AllowAnonymous]
    public class Index : PageModel
    {
        public string Version;

        public void OnGet()
        {
            Version = typeof(Duende.IdentityServer.Hosting.IdentityServerMiddleware).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+').First();
        }
    }
}