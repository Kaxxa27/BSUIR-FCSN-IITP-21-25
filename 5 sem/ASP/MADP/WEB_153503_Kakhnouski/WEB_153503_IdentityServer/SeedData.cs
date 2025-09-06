using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using WEB_153503_IdentityServer.Data;
using WEB_153503_IdentityServer.Models;

namespace WEB_153503_IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(WebApplication app)
        {
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (roleMgr.FindByNameAsync(Config.Admin).Result == null)
                {
                    roleMgr.CreateAsync(new IdentityRole(Config.Admin));
                    roleMgr.CreateAsync(new IdentityRole(Config.Customer));
                }

                var user = userMgr.FindByNameAsync("user").Result;
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = "user",
                        Email = "user@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "11111111111",

                    };

                    var resultUser = userMgr.CreateAsync(user, "User123*").GetAwaiter().GetResult();
                    var resultRole = userMgr.AddToRoleAsync(user, Config.Customer).GetAwaiter().GetResult();

                    if (!resultUser.Succeeded)
                    {
                        throw new Exception(resultUser.Errors.First().Description);
                    }

                    resultUser = userMgr.AddClaimsAsync(user, new Claim[]{
                            new Claim(JwtClaimTypes.Name, user.UserName),
                            new Claim(JwtClaimTypes.GivenName, "user"),
                            new Claim(JwtClaimTypes.FamilyName, "userFio"),
                            new Claim(JwtClaimTypes.Role, Config.Customer)

                    }).Result;

                    if (!resultUser.Succeeded)
                    {
                        throw new Exception(resultUser.Errors.First().Description);
                    }
                    Log.Debug("user created");
                }
                else
                {
                    Log.Debug("user already exists");
                }

                var admin = userMgr.FindByNameAsync("admin").Result;
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "22222222"

                    };

                    var resultAdmin = userMgr.CreateAsync(admin, "Admin123*").GetAwaiter().GetResult();
                    var resultRole = userMgr.AddToRoleAsync(admin, Config.Admin).GetAwaiter().GetResult();

                    if (!resultAdmin.Succeeded)
                    {
                        throw new Exception(resultAdmin.Errors.First().Description);
                    }

                    resultAdmin = userMgr.AddClaimsAsync(admin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "admin"),
                            new Claim(JwtClaimTypes.GivenName, "admin"),
                            new Claim(JwtClaimTypes.FamilyName, "adminfio"),
                            new Claim(JwtClaimTypes.Role, Config.Admin),

                    }).Result;

                    if (!resultAdmin.Succeeded)
                    {
                        throw new Exception(resultAdmin.Errors.First().Description);
                    }
                    Log.Debug("admin created");
                }
                else
                {
                    Log.Debug("admin already exists");
                }
            }
        }
    }
}