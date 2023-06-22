using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
