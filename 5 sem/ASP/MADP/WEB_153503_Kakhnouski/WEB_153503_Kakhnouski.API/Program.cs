global using System.Collections.Generic;
global using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.API.Services.CarCategoryService;
using WEB_153503_Kakhnouski.API.Services.CarService;
using WEB_153503_Kakhnouski.API.Services;

namespace WEB_153503_Kakhnouski.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"Enter 'Bearer' [space] and your token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {   
                {
                     new OpenApiSecurityScheme
                     {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme="oauth2",
                        Name="Bearer",
                        In = ParameterLocation.Header

                     },
                    new List<string>()
                }
            });
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<ICarCategoryService, CarCategoryService>();
        builder.Services.AddScoped<ICarService, CarService>();


        // Database configuration.
        var connection = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));

        // IdentityServer
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration.GetSection("IdentityServerUri").Value;
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" }; // Access Token + JWT
            });

		// For check Scope
		//builder.Services.AddAuthorization(options =>
		//{
		//    options.AddPolicy("ApiScope", policy =>
		//    {
		//        policy.RequireAuthenticatedUser();
		//        policy.RequireClaim("scope", "WEB");
		//    });
		//});

		builder.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyHeader()
					   .AllowAnyMethod();
			});
		});

		var app = builder.Build();

        //await DbInitializer.SeedData(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseStaticFiles();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}