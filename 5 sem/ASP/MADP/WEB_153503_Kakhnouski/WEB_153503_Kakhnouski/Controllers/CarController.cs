using Microsoft.AspNetCore.Mvc;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.Extensions;
using WEB_153503_Kakhnouski.Services.CarService;
using WEB_153503_Kakhnouski.Services.CategoryService;

namespace WEB_153503_Kakhnouski.Controllers;

public class CarController : Controller
{
    private readonly ICarCategoryService _carCategoryService;
    private readonly ICarService _carService;

    public CarController(ICarCategoryService carCategoryService, ICarService carService)
    {
        _carCategoryService = carCategoryService;
        _carService = carService;
    }

    [Route("CarsCatalog")]
    public async Task<IActionResult> Index(string? category, int pageNo = 1)
    {
        var categoryResponse = await _carCategoryService.GetCategoryListAsync();
        if (!categoryResponse.Success)
            return NotFound(categoryResponse.ErrorMessage);

        ViewData["categories"] = categoryResponse.Data;
        ViewData["currentCategory"] = categoryResponse.Data.SingleOrDefault(c => c.NormalizedName == category);

        var productResponse = await _carService.GetCarListAsync(category, pageNo);
        if (!productResponse.Success)
            return NotFound(productResponse.ErrorMessage);

        if (Request.IsAjaxRequest())
        {
            ListModel<Car> data = productResponse.Data!;
            return PartialView("_ProductIndexPartial", new
            {
                data.Items,
                data.CurrentPage,
                data.TotalPages,
                CategoryNormalizedName = category
            });
        }
        else return View(productResponse.Data);
        
    }
}
