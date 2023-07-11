using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IContactService _contactService;
        public ContactController(AppDbContext context, IContactService contactService)
        {
            _context = context;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllAsync();
            return View(contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Contact dbcontact = await _contactService.GetByIdAsync((int)id);
            if (dbcontact is null) return NotFound();
            return View(dbcontact);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Contact dbcontact = await _contactService.GetByIdAsync((int)id);
                if (dbcontact is null) return NotFound();

                _context.Contacts.Remove(dbcontact);
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
