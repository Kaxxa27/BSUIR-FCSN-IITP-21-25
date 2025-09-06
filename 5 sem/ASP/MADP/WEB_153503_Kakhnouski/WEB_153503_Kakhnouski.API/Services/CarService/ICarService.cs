using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;

namespace WEB_153503_Kakhnouski.API.Services.CarService;

public interface ICarService
{
    /// <summary>
    /// Getting a list of all objects
    /// </summary>
    /// <param name="categoryNormalizedName">Normalized category name for filtering</param>
    /// <param name="pageNo">List page number</param>
    /// <param name="pageSize">Number of objects on the page</param>
    /// <returns></returns>
    public Task<ResponseData<ListModel<Car>>> GetCarListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize=3);

    /// <summary>
    /// Object search by Id
    /// </summary>
    /// <param name="id">Object ID</param>
    /// <returns>Found object or null if the object is not found</returns>
    public Task<ResponseData<Car>> GetCarByIdAsync(int id);

    /// <summary>
    /// Update object
    /// </summary>
    /// <param name="id">Id the object being modified</param>
    /// <param name="car">Object with new parameters</param>
    /// <param name="formFile">Image file</param>
    /// <returns></returns>
    public Task UpdateCarAsync(int id, Car car);

    /// <summary>
    /// Delete odject
    /// </summary>
    /// <param name="id">Id the object being deleted</param>
    /// <returns></returns>
    public Task DeleteCarAsync(int id);
    
    /// <summary>
    /// Create object
    /// </summary>
    /// <param name="car">New object</param>
    /// <param name="formFile">Image file</param>
    /// <returns>Created object</returns>
    public Task<ResponseData<Car>> CreateCarAsync(Car car);

    /// <summary>
    /// Save image file for object
    /// </summary>
    /// <param name="id">Id object</param>
    /// <param name="formFile">Image file</param>
    /// <returns>Url to image file</returns>
    public Task<ResponseData<string>>  SaveImageAsync(int car, IFormFile? formFile);
}
