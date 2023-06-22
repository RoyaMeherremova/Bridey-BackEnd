using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
