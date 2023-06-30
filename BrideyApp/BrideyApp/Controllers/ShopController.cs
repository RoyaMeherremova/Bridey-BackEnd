using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly ISizeService _sizeService;
        private readonly IColorService _colorService;
        private readonly ICompositionService _compositionService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        public ShopController(ILayoutService layoutService,
                              ISizeService sizeService, 
                              IColorService colorService,
                              ICompositionService compositionService,
                              IBrandService brandService,
                              ICategoryService categoryService,
                              AppDbContext context)
        {
            _layoutService = layoutService;
            _sizeService = sizeService;
            _colorService = colorService;
            _compositionService = compositionService;
            _brandService = brandService;
            _categoryService = categoryService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Size> sizes = await _sizeService.GetAll();
            List<Color> colors = await _colorService.GetAll();
            List<Brand> brands = await _brandService.GetAll();
            Sale sale = await _context.Sales.FirstOrDefaultAsync();
            List<Category> categories = await _categoryService.GetAll();
            List<Composition> compositions = await _compositionService.GetAll();



            ShopVM model = new()
            {
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
                Sizes= sizes,
                Colors= colors,
                Compositions = compositions,
                Brands = brands,
                Categories = categories,
                Sale= sale,
            };
            return View(model);
        }
    }
}
