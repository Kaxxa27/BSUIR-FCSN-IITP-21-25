using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153503_Kakhnouski.Models;

namespace WEB_153503_Kakhnouski.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var selectListDemo = new List<ListDemo>()
            {
                new(){ Id = 1, Name = "item 1"},
                new(){ Id = 2, Name = "item 2"},
                new(){ Id = 3, Name = "item 3"},
                new(){ Id = 4, Name = "item 4"},
                new(){ Id = 5, Name = "item 5"},
            };

            var selectList = new SelectList(selectListDemo, "Id", "Name");
            return View(selectList);
        }
    }
}
