using TaxiApplication.Domain.Entity;

namespace TaxiApplication.WEB.Areas.User.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ITaxiOrderService _taxiOrderService;

        public AdminController(IClientService clientService, ITaxiOrderService taxiOrderService)
        {
            _clientService = clientService;
            _taxiOrderService = taxiOrderService;
        }

        #region HttpGet
        [HttpGet]
        public IActionResult Index()
        {
            return View(_clientService.GetAllClients().Result.Data);
        }

        [HttpGet]
        public IActionResult AddClient() { return View(); }

        [HttpGet]
        public IActionResult UpdateClient(int id) 
        { 
            return View(_clientService.FirstOrDefault(x => x.Id == id).Result.Data); 
        }

        [HttpGet]
        public IActionResult DeleteClient() { return View(); }  
        
        [HttpGet]
        public IActionResult AdminPanel() { return View(_clientService.GetAllClients().Result.Data); }

        [HttpGet]
        public IActionResult FindClient() { return View(); }

        [HttpGet]
        public IActionResult GetTaxiOrdersForAdmin(int userId)
        {
            return View(_taxiOrderService.GetAllClientTaxiOrders(userId).Result.Data);
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public async Task<IActionResult> AddClient(TaxiApplication.Domain.Entity.Client client)
        {
            if (ModelState.IsValid)
            {
                var response = await _clientService.AddClient(client);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", response.Description);
            }
            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var response = await _clientService.DeleteClient(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _taxiOrderService.DeleteTaxiOrder(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home", new { area = ""});
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient(TaxiApplication.Domain.Entity.Client client)
        {
            var response = await _clientService.UpdateClient(client);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("AdminPanel");
            }
            return View();
        }
        #endregion
    }
}
