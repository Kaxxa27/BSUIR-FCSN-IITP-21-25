using Microsoft.EntityFrameworkCore;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.API.Services.CarService;

namespace WEB_153503_Kakhnouski.API.Services.CarService;

public class CarService : ICarService
{
    public int MaxPageSize { get; } = 20;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CarService(AppDbContext context, IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseData<Car>> CreateCarAsync(Car car)
    {
        _context.Cars.Add(car);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new ResponseData<Car>
            {
                Success = false,
                ErrorMessage = ex.Message,
            };
        }

        return new ResponseData<Car>
        {
            Data = car
        };
    }


    public async Task DeleteCarAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car is null)
        {
            throw new Exception("Car was not found");
        }

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
    }

    public async Task<ResponseData<Car>> GetCarByIdAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car is null)
        {
            return new()
            {
                Success = false,
                ErrorMessage = "Car was not found"
            };
        }

        return new()
        {
            Data = car
        };
    }

    public async Task<ResponseData<ListModel<Car>>> GetCarListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
    {
        if (pageSize > MaxPageSize)
            pageSize = MaxPageSize;

        var query = _context.Cars.AsQueryable();
        var dataList = new ListModel<Car>();

        query = query.Where(d => categoryNormalizedName == null ||
                            d.Category!.NormalizedName.Equals(categoryNormalizedName))
                     .Include(t => t.Category);

        var count = query.Count();
        if (count == 0)
        {
            return new ResponseData<ListModel<Car>>
            {
                Data = dataList
            };
        }

        int totalPages = (int)Math.Ceiling(count / (double)pageSize);
        if (pageNo > totalPages)
        {
            return new ResponseData<ListModel<Car>>
            {
                Data = null,
                Success = false,
                ErrorMessage = "No such page"
            };
        }

        dataList.Items = await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        dataList.CurrentPage = pageNo;
        dataList.TotalPages = totalPages;
        return new ResponseData<ListModel<Car>>
        {
            Data = dataList
        };
    }

    public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile? formFile)
    {
        var responseData = new ResponseData<string>();
        var car = await _context.Cars.FindAsync(id);
        if (car is null)
        {
            return new ResponseData<string>
            {
                Success = false,
                ErrorMessage = "Car was not found"
            };
        }

        var host = "https://" + _httpContextAccessor.HttpContext?.Request.Host;
        var imageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

        if (formFile != null)
        {
            if (!string.IsNullOrEmpty(car.Image))
            {
                var prevImage = Path.GetFileName(car.Image);
                var prevImagePath = Path.Combine(imageFolder, prevImage);
                if (File.Exists(prevImagePath))
                {
                    File.Delete(prevImagePath);
                }
            }
            var ext = Path.GetExtension(formFile.FileName);
            var fName = Path.ChangeExtension(Path.GetRandomFileName(), ext);
            var filePath = Path.Combine(imageFolder, fName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            car.Image = $"{host}/images/{fName}";
            await _context.SaveChangesAsync();
        }
        responseData.Data = car.Image;
        return responseData;
    }

    public async Task UpdateCarAsync(int id, Car car)
    {
        var oldCar = await _context.Cars.FindAsync(id);
        if (oldCar is null)
        {
            throw new Exception("Car was not found");
        }

        oldCar.Name = car.Name;
        oldCar.Description = car.Description;
        oldCar.Price = car.Price;
        oldCar.Image = car.Image;
        oldCar.Category = car.Category;

        await _context.SaveChangesAsync();
    }
}
