using BrideyApp.Areas.Admin.ViewModels.User;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;

        public SubscribeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Subscribe> subscribes = await _context.Subscribes.ToListAsync();

            return View(subscribes);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Subscribe dbSubscribe = await _context.Subscribes.FirstOrDefaultAsync(m=>m.Id == id);
                if (dbSubscribe == null) return NotFound();

                _context.Subscribes.Remove(dbSubscribe);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
