using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ILayoutService _layoutService;
        public WishlistController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IActionResult> Index()
        {
            WishlistVM model = new()
            {
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
            };
            return View(model);
        }
    }
}
