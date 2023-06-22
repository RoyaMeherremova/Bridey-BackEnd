using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
