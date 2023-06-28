using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILayoutService _layoutService;

        public ShopController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IActionResult> Index()
        {
            ShopVM model = new()
            {
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
            };
            return View(model);
        }
    }
}
