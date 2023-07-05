using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Wishlist;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ILayoutService _layoutService;
        private readonly IProductService _productService;
        private readonly IWishlistService _wishlistService;
        public WishlistController(ILayoutService layoutService, IProductService productService, IWishlistService wishlistService)
        {
            _layoutService = layoutService;
            _productService = productService;
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Index()
        {
            //WishlistVM model = new()
            //{
            //    SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
            //};
            return View();
        }
    }
}
