using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
