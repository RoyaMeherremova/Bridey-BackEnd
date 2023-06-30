using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISizeService _sizeService;
        public SizeController(AppDbContext context, ISizeService sizeService)
        {
            _context = context;
            _sizeService = sizeService;
        }

        public async Task<IActionResult> Index()
        {
            List<Size> sizes = await _sizeService.GetAll();
            return View(sizes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SizeCreateVM size)
        {
            try
            {
                Size newSize = new()
                {
                    Name = size.Name,

                };
                await _context.Sizes.AddAsync(newSize);
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
                Size dbSize = await _sizeService.GetSizeById(id);
                if (dbSize == null) return NotFound();

                _context.Sizes.Remove(dbSize);
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
                Size dbSize = await _sizeService.GetSizeById(id);
                if (dbSize == null) return NotFound();

                SizeUpdateVM model = new()
                {
                    Name = dbSize.Name

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
        public async Task<IActionResult> Edit(int? id, SizeUpdateVM size)
        {
            try
            {
                if (id == null) return BadRequest();
                Size dbSize = await _sizeService.GetSizeById(id);
                if (dbSize == null) return NotFound();

                dbSize.Name = size.Name;

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
