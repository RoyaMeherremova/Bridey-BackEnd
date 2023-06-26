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
    public class SocialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISocialService _socialService;
        public SocialController(AppDbContext context, ISocialService socialService)
        {
            _context = context;
            _socialService = socialService;
        }
        public async Task<IActionResult> Index()
        {
            List<Social> socials = await _socialService.GetAll();
            return View(socials);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Social social = await _socialService.GetSocialById(id);
                if (social == null) return NotFound();
                return View(social);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }


        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialCreateVM social)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(social);
                }
                Social newSocial = new()
                {
                    Icon = social.Icon,
                    Link = social.Link,
                };
                await _context.Socials.AddAsync(newSocial);
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
                Social dbSocial = await _socialService.GetSocialById(id);
                if (dbSocial == null) return NotFound();

                _context.Socials.Remove(dbSocial);
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
                Social dbSocial = await _socialService.GetSocialById(id);
                if (dbSocial == null) return NotFound();

                SocialUpdateVM model = new()
                {
                    Icon = dbSocial.Icon,
                    Link = dbSocial.Link,

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
        public async Task<IActionResult> Edit(int? id, SocialUpdateVM social)
        {
            try
            {
                if (id == null) return BadRequest();
                Social dbSocial = await _socialService.GetSocialById(id);
                if (dbSocial == null) return NotFound();

                dbSocial.Icon = social.Icon;
                dbSocial.Link = social.Link;

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
