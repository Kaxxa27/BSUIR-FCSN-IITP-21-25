using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.Services.CategoryService;

namespace WEB_153503_Kakhnouski.Services.CarService;

public class MemoryCarService : ICarService
{
    private List<Car> _cars;
    private List<Category> _categories;
    IConfiguration _configuration;

    public MemoryCarService(ICarCategoryService carCategoryService, IConfiguration configuration)
    {
        _categories = carCategoryService.GetCategoryListAsync().Result.Data;
        SetupData();
        _configuration = configuration;

    }

    /// <summary>
    /// Initializing lists
    /// </summary>
    private void SetupData()
    {
        _cars = new List<Car>
        {
            new Car
            {
                Id = 1,
                Name = "BMW E38",
                Description = "Bumer",
                Image = "images/bmw_e38.jpg",
                Price = 5000,
                Category = _categories.Find(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Id = 2,
                Name = "Mersedes W140",
                Description = "Kaban",
                Image = "images/mersedes_w140.jpg",
                Price = 10000,
                Category = _categories.Find(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Id = 3,
                Name = "Porshe Panamera",
                Description = "Panama",
                Image = "images/porshe_panamera.jpeg",
                Price = 123456,
                Category = _categories.Find(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Id = 4,
                Name = "Porshe 911",
                Description = "911",
                Image = "images/porshe_911.jpg",
                Price = 3444,
                Category = _categories.Find(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Id = 5,
                Name = "Moskvich 412",
                Description = "Legenda",
                Image = "images/moskvich_412.jpg",
                Price = 50,
                Category = _categories.Find(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Id = 6,
                Name = "Yamaha",
                Description = "Minus Bashka",
                Image = "images/yamaha.jpg",
                Price = 15000,
                Category = _categories.Find(c => c.NormalizedName.Equals("moto"))
            },
            new Car
            {
                Id = 7,
                Name = "Ural",
                Description = "Ural",
                Image = "images/ural.jpg",
                Price = 350,
                Category = _categories.Find(c => c.NormalizedName.Equals("moto"))
            },
            new Car
            {
                Id = 8,
                Name = "Minsk",
                Description = "Minsk",
                Image = "images/minsk.jpg",
                Price = 1000,
                Category = _categories.Find(c => c.NormalizedName.Equals("moto"))
            },
            new Car
            {
                Id = 9,
                Name = "Kraz",
                Description = "Ammo",
                Image = "images/kraz.jpg",
                Price = 6666,
                Category = _categories.Find(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Id = 10,
                Name = "Jeep",
                Description = "Jeep",
                Image = "images/jeep.jpeg",
                Price = 23000,
                Category = _categories.Find(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Id = 11,
                Name = "Hamvi",
                Description = "Hamlo",
                Image = "images/hamvi.jpg",
                Price = 32000,
                Category = _categories.Find(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Id = 12,
                Name = "Kamaz",
                Description = "Pribul",
                Image = "images/kamaz.jpg",
                Price = 150000,
                Category = _categories.Find(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Id = 13,
                Name = "Crusak 100",
                Description = "Bandit",
                Image = "images/crusak_100.jfif",
                Price = 77000,
                Category = _categories.Find(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Id = 14,
                Name = "Mersedes G-class",
                Description = "Prosto Gelik",
                Image = "images/gelik.jpg",
                Price = 50000,
                Category = _categories.Find(c => c.NormalizedName.Equals("trucks"))
            },
            new Car
            {
                Id = 15,
                Name = "Ikarus",
                Description = "ikra",
                Image = "images/ikarus.jpg",
                Price = 40000,
                Category = _categories.Find(c => c.NormalizedName.Equals("bus"))
            },
            new Car
            {
                Id = 16,
                Name = "Laz 695",
                Description = "Автобус горит, да и ...",
                Image = "images/laz_695.jpg",
                Price = 5000,
                Category = _categories.Find(c => c.NormalizedName.Equals("bus"))
            },

        };
    }

    public Task<ResponseData<Car>> CreateCarAsync(Car car, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCarAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<Car>> GetCarByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<ListModel<Car>>> GetCarListAsync(string? categoryNormalizedName, int pageNo = 1)
    {
        var result = new ResponseData<ListModel<Car>>();

        if (Int32.TryParse(_configuration["ItemsPerPage"], out int itemsPerPage))
        {
            var cars = _cars.Where(c => categoryNormalizedName == null || c.Category.NormalizedName.Equals(categoryNormalizedName)).ToList();
            result.Data = new()
            {
                Items = cars.Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
                CurrentPage = pageNo,
                TotalPages = (int)Math.Ceiling((double)cars.Count / itemsPerPage)
            };
        }
        else
        {
            result.Success = false;
            result.ErrorMessage = "Invalid \"ItemsPerPage\" value";
        }
        return Task.FromResult(result);
    }

    public Task UpdateCarAsync(int id, Car car, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}
