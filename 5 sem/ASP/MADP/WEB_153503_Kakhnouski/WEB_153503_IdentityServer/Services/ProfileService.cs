using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WEB_153503_IdentityServer.Models;

namespace WEB_153503_IdentityServer.Services;

public class ProfileService : IProfileService
{
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly UserManager<ApplicationUser> _userMgr;
    private readonly RoleManager<IdentityRole> _roleMgr;

    public ProfileService
    (
        UserManager<ApplicationUser> userMgr,    
        RoleManager<IdentityRole> roleMgr,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory    
    )
    {
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _userMgr = userMgr;
        _roleMgr = roleMgr;
    }
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        string sub = context.Subject.GetSubjectId();
        var user = await _userMgr.FindByIdAsync(sub);

        var userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);
        var claims = userClaims.Claims.ToList();
        claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

        if (_userMgr.SupportsUserRole)
        {
            var roles = await _userMgr.GetRolesAsync(user);

            foreach( var rolename in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, rolename));
          
                if(_roleMgr.SupportsRoleClaims)
                {
                    var role = await _roleMgr.FindByIdAsync(rolename); 
                    if(role != null)
                    {
                        claims.AddRange(await _roleMgr.GetClaimsAsync(role));
                    }
                }
            }
        }

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        string sub = context.Subject.GetSubjectId();
        var user = await _userMgr.FindByIdAsync(sub);

        context.IsActive = user != null;
    }
}
