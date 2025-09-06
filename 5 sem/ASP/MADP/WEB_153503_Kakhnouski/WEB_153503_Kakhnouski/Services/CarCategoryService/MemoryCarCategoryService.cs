using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.Services.CategoryService;

namespace WEB_153503_Kakhnouski.Services.CarCategoryService;

public class MemoryCarCategoryService : ICarCategoryService
{
    public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var categories = new List<Category>() 
        { 
            new Category {Id=1, Name="A", 
                NormalizedName="moto"},
            new Category {Id=2, Name="B", 
                NormalizedName="cars"},
            new Category {Id=3, Name="C", 
                NormalizedName="trucks"},
            new Category {Id=4, Name="D", 
                NormalizedName="bus"},
        };

        return new ResponseData<List<Category>>() { Data = categories };
    }
}
