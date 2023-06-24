using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPositionService _positionService;

        public PositionController(AppDbContext context, IPositionService positionService)
        {
            _context = context;
            _positionService = positionService;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _positionService.GetAll();
            return View(positions);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Position position = await _positionService.GetPositionById(id);
            if (position == null) return NotFound();
            return View(position);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PositionCreateVM position)
        {
            try
            {
                Position newPosition = new()
                {
                    Name = position.Name,

                };
                await _context.Positions.AddAsync(newPosition);
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
                Position dbPosition = await _positionService.GetPositionById(id);
                if (dbPosition == null) return NotFound();

                _context.Positions.Remove(dbPosition);
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
            if (id == null) return BadRequest();
            Position dbPosition = await _positionService.GetPositionById(id);
            if (dbPosition == null) return NotFound();

            PositionUpdateVM model = new()
            {
                Name = dbPosition.Name
           
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, PositionUpdateVM position)
        {
            try
            {
                if (id == null) return BadRequest();
                Position dbPosition = await _positionService.GetPositionById(id);
                if (dbPosition == null) return NotFound();

                dbPosition.Name = position.Name;

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
