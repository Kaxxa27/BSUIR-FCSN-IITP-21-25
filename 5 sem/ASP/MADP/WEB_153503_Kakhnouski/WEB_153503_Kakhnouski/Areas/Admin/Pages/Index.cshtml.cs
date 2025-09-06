using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Services.CarService;

namespace WEB_153503_Kakhnouski.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICarService _carService;
        public IndexModel(ICarService carService)
        {
            _carService = carService;
        }


        public IList<Car> Cars { get; set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNo = 1)
        {
            var responce = await _carService.GetCarListAsync(null, pageNo);

            if (!responce.Success)
                return NotFound(responce.ErrorMessage ?? "");


            Cars = responce.Data?.Items!;
            CurrentPage = responce.Data?.CurrentPage ?? 0;      
            TotalPages = responce.Data?.TotalPages ?? 0;
            return Page();

        }
    }
}
