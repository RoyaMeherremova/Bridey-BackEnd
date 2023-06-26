using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeaderBackgroundController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHeaderBackgroundService _headerBackgroundService;

        public HeaderBackgroundController(AppDbContext context, IHeaderBackgroundService headerBackgroundService)
        {
            _context = context;
            _headerBackgroundService = headerBackgroundService;
        }
        public async Task<IActionResult> Index()
        {
            List<HeaderBackground> headerBackgrounds = await _headerBackgroundService.GetHeaderBackgroundsAsync();

            return View(headerBackgrounds);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                HeaderBackground dbHeaderBackground = await _headerBackgroundService.GetHeaderBackgroundByIdAsync(id);
                HeaderBackground model = new()
                {
                    Value = dbHeaderBackground.Value,
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
        public async Task<IActionResult> Edit(int? id, HeaderBackground updatedHeaderBackground)
        {
            try
            {
                if (id == null) return BadRequest();
                HeaderBackground dbHeaderBackground = await _headerBackgroundService.GetHeaderBackgroundByIdAsync(id);
                if (dbHeaderBackground is null) return NotFound();

                HeaderBackground model = new()
                {
                    Value = dbHeaderBackground.Value,
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                dbHeaderBackground.Value = updatedHeaderBackground.Value;
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
