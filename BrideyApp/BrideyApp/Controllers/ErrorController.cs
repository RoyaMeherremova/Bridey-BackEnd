using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IAdvertisingService _advertisingService;
        public ErrorController(IAdvertisingService advertisingService)
        {
            _advertisingService = advertisingService;
        }

        public async Task<IActionResult> Index()
        {
            List<Advertising> advertisings = await _advertisingService.GetAll();
            return View(advertisings);
        }
    }
}
