using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using BrideyApp.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly ILayoutService _layoutService;
        public BasketController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IActionResult> Index()
        {
            BasketVM model = new()
            {
                SectionBackgroundImages =  _layoutService.GetSectionBackgroundImages(),
            };
            return View(model);
        }
    }
}
