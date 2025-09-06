using Serilog;
using WEB_153503_Kakhnouski.Domain.Models.Cart;
using WEB_153503_Kakhnouski.Middleware;
using WEB_153503_Kakhnouski.Models;
using WEB_153503_Kakhnouski.Services.CarCategoryService;
using WEB_153503_Kakhnouski.Services.CarService;
using WEB_153503_Kakhnouski.Services.CartService;
using WEB_153503_Kakhnouski.Services.CategoryService;

namespace WEB_153503_Kakhnouski
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddScoped<ICarCategoryService, ApiCarCategoryService>();
            builder.Services.AddScoped<ICarService, ApiCarService>();
            builder.Services.AddScoped<Cart, SessionCart>();


            UriData uriData = builder.Configuration.GetSection("UriData").Get<UriData>()!;

            builder.Services.AddHttpClient<ICarService, ApiCarService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));
            builder.Services.AddHttpClient<ICarCategoryService, ApiCarCategoryService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));


            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = "cookie";
                opt.DefaultChallengeScheme = "oidc";
               // opt.DefaultChallengeScheme = "bearer";
            })
            .AddCookie("cookie")
            //.AddJwtBearer("bearer")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority =
                builder.Configuration["IdentityServerSettings:AuthorityUrl"];
                options.ClientId =
                builder.Configuration["IdentityServerSettings:ClientId"];
                options.ClientSecret =
                builder.Configuration["IdentityServerSettings:ClientSecret"];

                // Получить Claims пользователя
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ResponseType = "code";
                options.ResponseMode = "query";           

                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";

                //options.Scope.Add(builder.Configuration["IdentityServerSettings:ClientId"]!);
                options.Scope.Clear();
                options.Scope.Add("WEB");
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                

                //options.Scope.Add(builder.Configuration["IdentityServerSettings:Scopes"]!);
                //List<string> sc = new() { "WEB", "openid", "profile", "email" };
                //options.Scope.AddRange<string>(sc);
                options.SaveTokens = true;
            });

            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("admin", b => b.RequireClaim("role", "admin"));
            });

            var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

   
            app.UseRouting();
            app.MapRazorPages().RequireAuthorization();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseMiddleware<LogMiddleware>(logger);

            app.Run();
        }
    }
}