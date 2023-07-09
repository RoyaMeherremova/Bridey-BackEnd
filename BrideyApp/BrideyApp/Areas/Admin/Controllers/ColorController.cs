using BrideyApp.Areas.Admin.ViewModels.Color;
using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Color = BrideyApp.Models.Color;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IColorService _colorService;

        public ColorController(AppDbContext context, IColorService colorService)
        {
            _context = context;
            _colorService = colorService;
        }

        public async Task<IActionResult> Index()
        {
            List<Color> colors = await _colorService.GetAll();
            return View(colors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColorCreateVM color)
        {
            try
            {
                Color newColor = new()
                {
                    Name = color.Name,

                };
                await _context.Colors.AddAsync(newColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Color dbColor = await _colorService.GetColorById(id);
                if (dbColor == null) return NotFound();

                _context.Colors.Remove(dbColor);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Color dbColor = await _colorService.GetColorById(id);
                if (dbColor == null) return NotFound();

                ColorUpdateVM model = new()
                {
                    Name = dbColor.Name

                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ColorUpdateVM color)
        {
            try
            {
                if (id == null) return BadRequest();
                Color dbColor = await _colorService.GetColorById(id);
                if (dbColor == null) return NotFound();

                dbColor.Name = color.Name;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
