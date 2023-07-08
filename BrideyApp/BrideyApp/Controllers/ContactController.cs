using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly IAdvertisingService _advertisingService;


        public ContactController(ILayoutService layoutService, IAdvertisingService advertisingService, AppDbContext context)
        {
            _layoutService = layoutService;
            _advertisingService = advertisingService;
            _context = context;
        }

        public async Task< IActionResult> Index()
        {
            List<Advertising> advertisings = await _advertisingService.GetAll();

            ContactVM model = new()
            {
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
                Settings = _layoutService.GetSettingDatas(),
                Advertisings= advertisings

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(ContactVM model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", model);
            Contact contact = new()
            {
                FirstName = model.FirstName,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message,
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
