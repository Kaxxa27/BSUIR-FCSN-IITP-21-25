using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Services.CarService;

namespace WEB_153503_Kakhnouski.Areas.Admin.Pages;

public class CreateModel : PageModel
{
    private readonly ICarService _carService;

    public CreateModel(ICarService carService)
    {
        _carService = carService;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Car Car { get; set; } = default!;

    [BindProperty]
    public IFormFile Image { get; set; } = default!;


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var responce = await _carService.CreateCarAsync(Car, Image);
        if (!responce.Success) 
        {
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
