using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly IHeaderBackgroundService _headerBackgroundService;
        private readonly IAboutBoxService _aboutBoxService;
        private readonly ITeamService _teamService;
        private readonly IAdvertisingService _advertisingService;






        public AboutController(AppDbContext context, 
                               ILayoutService layoutService, 
                               IHeaderBackgroundService headerBackgroundService,
                               IAboutBoxService aboutBoxService,
                               ITeamService teamService,
                               IAdvertisingService advertisingService)
        {
            _context = context;
            _layoutService = layoutService;
            _headerBackgroundService = headerBackgroundService;
            _aboutBoxService = aboutBoxService;
            _teamService = teamService;
            _advertisingService = advertisingService;
        }
        public async Task<IActionResult> Index()
        {
            AboutBanner aboutBanner = await _context.AboutBanners.FirstOrDefaultAsync();
            List<AboutBox> aboutBoxes = await _aboutBoxService.GetAll();
            List<Team> teams = await _teamService.GetAll();
            List<Advertising> advertisings = await _advertisingService.GetAll();

            AboutVM model = new()
            {
                AboutBanner = aboutBanner,
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
                HeaderBackgrounds = _headerBackgroundService.GetHeaderBackgroundDatas(),
                AboutBoxes= aboutBoxes,
                Teams= teams,
                Advertisings = advertisings,
            };
            return View(model);
        }
    }
}
