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
    public class DeleteModel : PageModel
    {
        private readonly ICarService _carService;

        public DeleteModel(ICarService carService)
        {
            _carService = carService;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responce = await _carService.GetCarByIdAsync(id.Value);

            if (!responce.Success)
            {
                return NotFound();
            }
            else 
            {
                Car = responce.Data!;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _carService.DeleteCarAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
