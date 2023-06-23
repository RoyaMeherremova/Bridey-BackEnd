using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeBannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IHomeBannerService _homeBannnerService;
        public HomeBannerController(AppDbContext context, IWebHostEnvironment env,IHomeBannerService homeBannerService)
        {
            _context = context;
            _env = env;
            _homeBannnerService=homeBannerService;
        }

        public async Task<IActionResult> Index()
        {
            HomeBanner homeBanner = await _context.HomeBanners.FirstOrDefaultAsync(); 
            return View(homeBanner);
        }

        //DETAIL
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            HomeBanner homeBanner = await _homeBannnerService.GetHomeBannerById(id);    
            if (homeBanner == null) return NotFound();
            return View(homeBanner);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

    }
}
