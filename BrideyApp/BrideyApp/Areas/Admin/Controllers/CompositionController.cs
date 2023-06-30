using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompositionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICompositionService _compositionService;
        public CompositionController(AppDbContext context, ICompositionService compositionService)
        {
            _context = context;
            _compositionService = compositionService;
        }

        public async Task<IActionResult> Index()
        {
            List<Composition> compositions = await _compositionService.GetAll();
            return View(compositions);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompositionCreateVM composition)
        {
            try
            {
                Composition newComposition = new()
                {
                    Name = composition.Name,

                };
                await _context.Compositions.AddAsync(newComposition);
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
                Composition dbComposition = await _compositionService.GetCompositionById(id);
                if (dbComposition == null) return NotFound();

                _context.Compositions.Remove(dbComposition);
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
                Composition dbComposition = await _compositionService.GetCompositionById(id);
                if (dbComposition == null) return NotFound();

                CompositionUpdateVM model = new()
                {
                    Name = dbComposition.Name

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
        public async Task<IActionResult> Edit(int? id, CompositionUpdateVM composition)
        {
            try
            {
                if (id == null) return BadRequest();
                Composition dbComposition = await _compositionService.GetCompositionById(id);
                if (dbComposition == null) return NotFound();

                dbComposition.Name = composition.Name;

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
