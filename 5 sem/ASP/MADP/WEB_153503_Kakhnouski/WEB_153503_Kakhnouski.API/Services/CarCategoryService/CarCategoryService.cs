using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.API.Services;
using WEB_153503_Kakhnouski.API.Services.CarService;

namespace WEB_153503_Kakhnouski.API.Services.CarCategoryService;

public class CarCategoryService : ICarCategoryService
{
    private readonly AppDbContext _dbContext;

    public CarCategoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        return new ResponseData<List<Category>>
        {
            Data = await _dbContext.Categories.ToListAsync()
        };
    }
}
