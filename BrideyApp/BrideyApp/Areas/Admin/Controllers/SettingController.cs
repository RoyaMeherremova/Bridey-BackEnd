using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISettingService _settingService;

        public SettingController(AppDbContext context, ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _settingService.GetSettingsAsync();

            return View(settings);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                Setting dbSetting = await _settingService.GetSettingByIdAsync(id);
                Setting model = new()
                {
                    Value = dbSetting.Value,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Setting updatedSetting)
        {
            try
            {
                if (id == null) return BadRequest();
                Setting dbSetting = await _settingService.GetSettingByIdAsync(id);
                if (dbSetting is null) return NotFound();

                Setting model = new()
                {
                    Value = dbSetting.Value,
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                dbSetting.Value = updatedSetting.Value;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                @ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}

