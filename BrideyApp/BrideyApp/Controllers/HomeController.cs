using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IBrideService _brideService;

        public HomeController(AppDbContext context, ISliderService sliderService, IHomeBannerService homeBannerService, IBrideService brideService)
        {
            _context = context;
            _sliderService = sliderService;
            _brideService = brideService;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAll();

            HomeBanner homeBanner = await _context.HomeBanners.FirstOrDefaultAsync();

            AboutUs aboutUs = await _context.AboutUss.FirstOrDefaultAsync();

            List<Bride> brides = await _brideService.GetAll();

            HomeVM model = new()
            {
                Sliders = sliders,
                HomeBanner = homeBanner,
                AboutUs = aboutUs,
                Brides = brides
            };

            return View(model);

        }


    }
}