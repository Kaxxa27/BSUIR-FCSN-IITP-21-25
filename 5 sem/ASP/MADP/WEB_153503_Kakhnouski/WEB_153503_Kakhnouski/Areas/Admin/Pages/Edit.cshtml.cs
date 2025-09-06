using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Services.CarService;

namespace WEB_153503_Kakhnouski.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICarService _carService;
        public EditModel(ICarService carService)
        {
            _carService = carService;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }
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
            Car = response.Data!;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _carService.UpdateCarAsync(Car.Id, Car, Image);

            return RedirectToPage("./Index");
        }

        private async Task<bool> ToolExists(int id)
        {
            var response = await _carService.GetCarByIdAsync(id);
            return response.Success;
        }
    }
}
