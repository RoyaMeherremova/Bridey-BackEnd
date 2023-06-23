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
        private readonly IHomeBannerService _homeBannerService;


        public HomeController(AppDbContext context, ISliderService sliderService, IHomeBannerService homeBannerService)
        {
            _context = context;
            _sliderService = sliderService;
            _homeBannerService = homeBannerService;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAll();

            HomeBanner homeBanners = await _context.HomeBanners.FirstOrDefaultAsync();


            HomeVM model = new()
            {
                Sliders = sliders,
                HomeBanner = homeBanners
            };
                
                return View(model);

        }

    
    }
}