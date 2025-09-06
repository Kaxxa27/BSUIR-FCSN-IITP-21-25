using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.API.Services.CarService;

namespace WEB_153503_Kakhnouski.Tests;


public class CarServiceTests : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<AppDbContext> _contextOptions;

    #region ConstructorAndDispose
    public CarServiceTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new AppDbContext(_contextOptions);

        context.Database.EnsureCreated();

        context.Categories.AddRange(
                new() { Id = 1, Name = "A", NormalizedName = "moto" },
                new() { Id = 2, Name = "B", NormalizedName = "cars" });

        context.SaveChanges();

        context.Cars.AddRange(
           new() { Id = 1, Name = "Mers", Price = 7600, Category = context.Categories.First(c => c.NormalizedName == "moto"), },
           new() { Id = 2, Name = "Opel", Price = 777, Category = context.Categories.First(c => c.NormalizedName == "cars") },
           new() { Id = 3, Name = "BMW", Price = 15400, Category = context.Categories.First(c => c.NormalizedName == "cars") });

        context.SaveChanges();
    }

    AppDbContext CreateContext() => new AppDbContext(_contextOptions);
    public void Dispose() => _connection.Dispose();
    #endregion

    #region ServiceTests
    [Fact]
    public void ServiceReturnsFirstPageOfThreeItems()
    {
        // Arrange.
        using var context = CreateContext();
        var service = new CarService(context, null!, null!, null!);

        // Act.
        var result = service.GetCarListAsync(null).Result;

        // Assert.
        Assert.IsType<ResponseData<ListModel<Car>>>(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data!.CurrentPage);
        Assert.Equal(3, result.Data!.Items.Count);
        Assert.Equal(1, result.Data!.TotalPages);
        Assert.Equal(context.Cars.First(), result.Data.Items[0]);
    }

    [Theory]
    [InlineData(1)]
    public void ServiceChoosesRightSpecifiedPage(int pageNo)
    {
        // Arrange.
        using var context = CreateContext();
        var service = new CarService(context, null!, null!, null!);

        // Act.
        var result = service.GetCarListAsync(null, pageNo).Result;

        // Assert.
        Assert.IsType<ResponseData<ListModel<Car>>>(result);
        Assert.True(result.Success);
        Assert.Equal(pageNo, result.Data!.CurrentPage);
        Assert.Equal(1, result.Data!.TotalPages);
        Assert.Equal(context.Cars.Skip((pageNo - 1) * 3).First(), result.Data.Items[0]);
    }

    [Fact]
    public void ServicePerformsCorrectFilteringByCategory()
    {
        // Arrange
        using var context = CreateContext();
        var service = new CarService(context, null!, null!, null!);

        // Act
        var result = service.GetCarListAsync("moto").Result;

        // Assert
        Assert.IsType<ResponseData<ListModel<Car>>>(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data!.CurrentPage);
        Assert.Equal(1, result.Data.Items.Count);
        Assert.Equal(1, result.Data.TotalPages);
        Assert.All(result.Data.Items, item => Assert.True(item.Category == context.Categories.First(c => c.NormalizedName == "moto")));
    }


    [Fact]
    public void ServiceDoesNotAllowToSetPageSizeMoreThanMax()
    {
        // Arrange
        using var context = CreateContext();
        var service = new CarService(context, null!, null!, null!);

        // Act
        var result = service.GetCarListAsync(null, 1, service.MaxPageSize + 1).Result;

        // Assert
        Assert.IsType<ResponseData<ListModel<Car>>>(result);
        Assert.True(result.Data!.Items.Count <= service.MaxPageSize);
    }

    [Fact]
    public void ServiceDoesNotAllowToSetPageNumberMoreThanPagesCount()
    {
        // Arrange
        using var context = CreateContext();
        var service = new CarService(context, null!, null!, null!);

        // Act
        var result = service.GetCarListAsync(null!, 100).Result;

        // Assert
        Assert.IsType<ResponseData<ListModel<Car>>>(result);
        Assert.False(result.Success);
    }

    #endregion

}
