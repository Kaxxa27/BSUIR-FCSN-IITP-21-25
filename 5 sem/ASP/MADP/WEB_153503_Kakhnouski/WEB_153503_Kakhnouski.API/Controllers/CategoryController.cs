using Microsoft.AspNetCore.Mvc;
using WEB_153503_Kakhnouski.API.Services.CarService;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.API.Services;

namespace WEB_153503_Kakhnouski.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICarCategoryService _carCategoryService;

        public CategoryController(ICarCategoryService carCategoryService)
        {
            _carCategoryService = carCategoryService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<ResponseData<List<Category>>>> GetCategories()
        {
            var result = await _carCategoryService.GetCategoryListAsync();
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
