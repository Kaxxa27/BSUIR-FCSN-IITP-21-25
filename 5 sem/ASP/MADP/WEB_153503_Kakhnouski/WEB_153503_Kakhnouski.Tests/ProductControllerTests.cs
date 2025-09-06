namespace WEB_153503_Kakhnouski.Tests;

public class CarControllerTests
{
    #region Index404
    [Fact]
    public void IndexReturns404WhenCategoriesAreReceiveUnsuccessfully()
    {
        // Arrange.
        var categoryService = new Mock<ICarCategoryService>();
        categoryService.Setup(m => m.GetCategoryListAsync())
            .ReturnsAsync(new ResponseData<List<Category>> { Success = false });

        var carService = new Mock<ICarService>();
        carService.Setup(m => m.GetCarListAsync(It.IsAny<string?>(), It.IsAny<int>()))
            .ReturnsAsync(new ResponseData<ListModel<Car>> { Success = true });

        var controller = new CarController(categoryService.Object, carService.Object);

        // Act.
        var result = controller.Index(null).Result;

        // Assert.
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public void IndexReturns404WhenCarsAreReceivedUnsuccessfully()
    {
        // Arrange.
        var categoryService = new Mock<ICarCategoryService>();
        categoryService.Setup(m => m.GetCategoryListAsync())
            .ReturnsAsync(new ResponseData<List<Category>>
            { Success = true, Data = new List<Category>() { new Category() { Name = "moto", NormalizedName = "moto" } } });

        var carService = new Mock<ICarService>();
        carService.Setup(m => m.GetCarListAsync(It.IsAny<string?>(), It.IsAny<int>()))
            .ReturnsAsync(new ResponseData<ListModel<Car>> { Success = false });

        var controller = new CarController(categoryService.Object, carService.Object);

        // Act.
        var result = controller.Index(null).Result;

        // Assert.
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
    #endregion

    #region ViewData
    private List<Category> TestCategories => new List<Category>()
    {
        new() { Id= 1, Name="A", NormalizedName="moto"},
        new() { Id = 2, Name="B", NormalizedName="cars" }
    };

    private List<Car> TestCars => new List<Car>()
    {
        new () { Id = 1, Name = "Mers", Price = 7600, Category = TestCategories.First(c => c.NormalizedName == "moto")},
        new () { Id = 2, Name = "BMW", Price = 15400, Category = TestCategories.First(c => c.NormalizedName == "moto")}
    };

    [Fact]
    public void IndexViewDataContainsCategories()
    {
        // Arrange.
        var categoryService = new Mock<ICarCategoryService>();
        categoryService.Setup(m => m.GetCategoryListAsync())
            .ReturnsAsync(new ResponseData<List<Category>>
            {
                Success = true,
                Data = TestCategories
            });

        var CarService = new Mock<ICarService>();
        CarService.Setup(m => m.GetCarListAsync(It.IsAny<string?>(), It.IsAny<int>()))
            .ReturnsAsync(new ResponseData<ListModel<Car>>
            {
                Success = true,
                Data = new() { Items = TestCars }
            });


        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(c => c.Request.Headers).Returns(new HeaderDictionary());

        var controller = new CarController(categoryService.Object, CarService.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            }
        };

        // Act.
        var result = controller.Index(null).Result;

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(TestCategories, (viewResult.ViewData["categories"] as List<Category>)!, new CategoryComparer());
    }

    [Fact]
    public void IndexViewDataContainsValidCurrentCategoryWhenCategoryParameterIsNull()
    {
        // Arrange.
        var categoryService = new Mock<ICarCategoryService>();
        categoryService.Setup(m => m.GetCategoryListAsync())
            .ReturnsAsync(new ResponseData<List<Category>>
            { Success = true, Data = new List<Category>() { new Category() { Name = "1", NormalizedName = "1" } } });

        var CarService = new Mock<ICarService>();
        CarService.Setup(m => m.GetCarListAsync(It.IsAny<string?>(), It.IsAny<int>()))
            .ReturnsAsync(new ResponseData<ListModel<Car>> { Success = true });

        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(c => c.Request.Headers).Returns(new HeaderDictionary());

        var controller = new CarController(categoryService.Object, CarService.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            }
        };

        // Act.
        var result = controller.Index(null).Result;

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewData["currentCategory"] as Category);
    }


    [Fact]
    public void IndexViewDataContainsValidCurrentCategoryWhenCategoryParameterIsNotNull()
    {
        // Arrange.
        var categoryService = new Mock<ICarCategoryService>();
        categoryService.Setup(m => m.GetCategoryListAsync())
            .ReturnsAsync(new ResponseData<List<Category>>
            {
                Success = true,
                Data = TestCategories
            });

        var CarService = new Mock<ICarService>();
        CarService.Setup(m => m.GetCarListAsync(It.IsAny<string?>(), It.IsAny<int>()))
            .ReturnsAsync(new ResponseData<ListModel<Car>>
            {
                Success = true,
                Data = new() { Items = TestCars }
            });


        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(c => c.Request.Headers).Returns(new HeaderDictionary());

        var controller = new CarController(categoryService.Object, CarService.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            }
        };

        // Act.
        var result = controller.Index("moto").Result;

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(TestCategories.First(c => c.NormalizedName == "moto"),
            (viewResult.ViewData["currentCategory"] as Category)!, new CategoryComparer());
    }

    [Fact]
    public void IndexRightModel()
    {
        // Arrange.
        var categoryService = new Mock<ICarCategoryService>();
        categoryService.Setup(m => m.GetCategoryListAsync())
            .ReturnsAsync(new ResponseData<List<Category>>
            {
                Success = true,
                Data = TestCategories
            });

        var model = new ListModel<Car>() { Items = TestCars };
        var CarService = new Mock<ICarService>();
        CarService.Setup(m => m.GetCarListAsync(It.IsAny<string?>(), It.IsAny<int>()))
            .ReturnsAsync(new ResponseData<ListModel<Car>>
            {
                Success = true,
                Data = model
            });


        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(c => c.Request.Headers).Returns(new HeaderDictionary());

        var controller = new CarController(categoryService.Object, CarService.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            }
        };

        // Act.
        var result = controller.Index(null).Result;

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        var modelResult = Assert.IsType<ListModel<Car>>(viewResult.Model);
        Assert.Equal(model, modelResult);
    }
    #endregion


}

class CategoryComparer : IEqualityComparer<Category>
{
    public bool Equals(Category? x, Category? y)
    {
        if (x == null || y == null)
            return false;


        if (x == y)
            return true;

        return x.Id == y.Id && x.Name == y.Name && x.NormalizedName == y.NormalizedName;
    }

    public int GetHashCode([DisallowNull] Category obj)
    {
        return HashCode.Combine(obj.Id, obj.Name, obj.NormalizedName);
    }
}

