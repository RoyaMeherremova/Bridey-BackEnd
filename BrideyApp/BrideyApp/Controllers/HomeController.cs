using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IBrideService _brideService;
        private readonly ITeamService _teamService;
        private readonly IAdvertisingService _advertisingService;
        private readonly ILayoutService _layoutService;
        private readonly IHeaderBackgroundService _headerBackgroundService;
        private readonly IBlogService _blogService;
        private readonly IProductService _productService;

        public HomeController(AppDbContext context,
                              ISliderService sliderService,
                              IHomeBannerService homeBannerService,
                              IBrideService brideService, 
                              ITeamService teamService,
                              IAdvertisingService advertisingService,
                              ILayoutService layoutService,
                              IHeaderBackgroundService headerBackgroundService,
                              IBlogService blogService,
                              IProductService productService)
        {
            _context = context;
            _sliderService = sliderService;
            _brideService = brideService;
            _teamService = teamService;
            _advertisingService = advertisingService;
            _layoutService = layoutService;
            _headerBackgroundService = headerBackgroundService;
            _blogService = blogService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAll();
            HomeBanner homeBanner = await _context.HomeBanners.FirstOrDefaultAsync();
            AboutUs aboutUs = await _context.AboutUss.FirstOrDefaultAsync();
            List<Bride> brides = await _brideService.GetAll();
            List<Team> teams = await _teamService.GetAll();
            List<Advertising> advertisings = await _advertisingService.GetAll();
            List<Blog> blogs = await _blogService.GetAll();
            List<Product> products = await _context.Products.Include(m=>m.Images).Include(p=>p.ProductCategories).Take(8).ToListAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                HomeBanner = homeBanner,
                AboutUs = aboutUs,
                Brides = brides,
                Teams = teams,
                Advertisings = advertisings,
                Settings = _layoutService.GetSettingDatas(),
                HeaderBackgrounds = _headerBackgroundService.GetHeaderBackgroundDatas(),
                Blogs = blogs,
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
                Products = products

            };

            return View(model);

        }


    }
}