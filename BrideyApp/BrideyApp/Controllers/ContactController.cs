using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILayoutService _layoutService;
        private readonly IAdvertisingService _advertisingService;


        public ContactController(ILayoutService layoutService, IAdvertisingService advertisingService)
        {
            _layoutService = layoutService;
            _advertisingService = advertisingService;
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
    }
}
