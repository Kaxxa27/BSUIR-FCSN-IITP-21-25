using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;

namespace WEB_153503_Kakhnouski.API.Services.CarService;

public interface ICarCategoryService
{
    /// <summary>
    /// Getting a list of all categories
    /// </summary>
    /// <returns> ResponseData<List<Category>> </returns>
    public Task<ResponseData<List<Category>>> GetCategoryListAsync();
}
