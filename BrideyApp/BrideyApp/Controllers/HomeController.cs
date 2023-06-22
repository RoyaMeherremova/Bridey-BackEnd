using BrideyApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    
    }
}