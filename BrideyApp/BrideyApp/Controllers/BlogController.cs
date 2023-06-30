using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILayoutService _layoutService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ICompositionService _compositionService;
        private readonly IAdvertisingService _advertisingService;

        public BlogController(ILayoutService layoutService,
                              IBlogService blogService, 
                              ICategoryService categoryService,
                              ICompositionService compositionService,
                              IAdvertisingService advertisingService)
        {
            _layoutService = layoutService;
            _blogService = blogService;
            _categoryService = categoryService;
            _compositionService = compositionService;
            _advertisingService = advertisingService;
        }

        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetAll();
            List<Category> categories = await _categoryService.GetAll();
            List<Composition> compositions = await _compositionService.GetAll();
            List<Advertising> advertisings = await _advertisingService.GetAll();




            BlogVM model = new()
            {
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
                Blogs = blogs,
                Categories = categories,
                Compositions = compositions,
                Advertisings= advertisings
            };
            return View(model);
        }
    }
}
