using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            AboutBanner aboutBanner = await _context.AboutBanners.FirstOrDefaultAsync();

            AboutVM model = new()
            {
                AboutBanner = aboutBanner
            };
            return View(model);
        }
    }
}
