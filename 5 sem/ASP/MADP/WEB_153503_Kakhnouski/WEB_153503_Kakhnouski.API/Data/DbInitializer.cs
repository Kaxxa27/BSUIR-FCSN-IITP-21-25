using Microsoft.EntityFrameworkCore;
using WEB_153503_Kakhnouski.Domain.Entities;

namespace WEB_153503_Kakhnouski.API.Data;

public class DbInitializer
{
    public static async Task SeedData(WebApplication app)
    {
        // Get DbContext.
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Do migrations.
        await context.Database.MigrateAsync();

        // Fill categories.
        await context.Categories.AddRangeAsync(new List<Category>()
        {
            new Category { Name="A", NormalizedName="moto"},
            new Category { Name="B", NormalizedName="cars"},
            new Category { Name="C", NormalizedName="trucks"},
            new Category { Name="D", NormalizedName="bus"},
        });

        await context.SaveChangesAsync();

        // Fill cars.
        string imageRoot = $"{app.Configuration["AppUrl"]!}/images";

        await context.Cars.AddRangeAsync(new List<Car>()
        {
            new Car
            {
                Name = "BMW E38",
                Description = "Bumer",
                Image = "images/bmw_e38.jpg",
                Price = 5000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Name = "Mersedes W140",
                Description = "Kaban",
                Image = "images/mersedes_w140.jpg",
                Price = 10000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Name = "Porshe Panamera",
                Description = "Panama",
                Image = "images/porshe_panamera.jpeg",
                Price = 123456,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Name = "Porshe 911",
                Description = "911",
                Image = "images/porshe_911.jpg",
                Price = 3444,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Name = "Moskvich 412",
                Description = "Legenda",
                Image = "images/moskvich_412.jpg",
                Price = 50,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Name = "Yamaha",
                Description = "Minus Bashka",
                Image = "images/yamaha.jpg",
                Price = 15000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("moto"))
            },
            new Car
            {
                Name = "Ural",
                Description = "Ural",
                Image = "images/ural.jpg",
                Price = 350,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("moto"))
            },
            new Car
            {
                Name = "Minsk",
                Description = "Minsk",
                Image = "images/minsk.jpg",
                Price = 1000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("moto"))
            },
            new Car
            {
                Name = "Kraz",
                Description = "Ammo",
                Image = "images/kraz.jpg",
                Price = 6666,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Name = "Jeep",
                Description = "Jeep",
                Image = "images/jeep.jpeg",
                Price = 23000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Name = "Hamvi",
                Description = "Hamlo",
                Image = "images/hamvi.jpg",
                Price = 32000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Name = "Kamaz",
                Description = "Pribul",
                Image = "images/kamaz.jpg",
                Price = 150000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Name = "Crusak 100",
                Description = "Bandit",
                Image = "images/crusak_100.jfif",
                Price = 77000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Name = "Mersedes G-class",
                Description = "Prosto Gelik",
                Image = "images/gelik.jpg",
                Price = 50000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Name = "Ikarus",
                Description = "ikra",
                Image = "images/ikarus.jpg",
                Price = 40000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("bus"))
            },
            new Car
            {
                Name = "Laz 695",
                Description = "Автобус горит, да и ...",
                Image = "images/laz_695.jpg",
                Price = 5000,
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("bus"))
            }
        });

        await context.SaveChangesAsync();
    }
}
