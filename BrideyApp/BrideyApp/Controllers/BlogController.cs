using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
