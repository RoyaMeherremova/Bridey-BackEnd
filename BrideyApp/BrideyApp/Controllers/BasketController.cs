using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
