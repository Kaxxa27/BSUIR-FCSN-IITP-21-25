using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Services.CarService;

namespace WEB_153503_Kakhnouski.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ICarService _carService;
        public DetailsModel(ICarService carService)
        {
            _carService = carService;
        }

        public Car Car { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _carService.GetCarByIdAsync(id.Value);
            if (!response.Success)
            {
                return NotFound();
            }
            else 
            {
                Car = response.Data!;
            }
            return Page();
        }
    }
}
