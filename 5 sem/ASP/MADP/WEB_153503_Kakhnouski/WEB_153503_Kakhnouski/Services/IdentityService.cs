using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WEB_153503_Kakhnouski.Services
{
    public class IdentityService
    {
        private readonly HttpContext _httpContext;

        public IdentityService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
        }

        public async Task SetUser()
        {
            var token = await _httpContext.GetTokenAsync("access_token");

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(token);

                var userClaims = tokenS.Claims;

                if (userClaims != null && userClaims.Any())
                {
                    var newClaimsIdentity = new ClaimsIdentity(userClaims, "jwt");
                    var userPrincipal = new ClaimsPrincipal(newClaimsIdentity);
                    _httpContext.User = userPrincipal;
                }
            }

            var res = _httpContext.User;
        }
    }
}
