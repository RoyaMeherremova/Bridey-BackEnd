using BrideyApp.Areas.Admin.ViewModels.SectionBackgroundImage;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SectionBackgroundImageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISectionBackgroundImageService _backgroundImageService;
        public SectionBackgroundImageController(AppDbContext context, 
                                                IWebHostEnvironment env,
                                                ISectionBackgroundImageService backgroundImageService)
        {
            _context = context;
            _env = env;
            _backgroundImageService = backgroundImageService;
        }
        public async Task<IActionResult> Index()
        {
            List<SectionBackgroundImage> backgroundImages = await _backgroundImageService.GetSectionBackgroundImageDatasAsync();
            return View(backgroundImages);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                SectionBackgroundImage dbBackgroundImage = await _backgroundImageService.GetSectionBackgroundImageByIdAsync(id);
                if (dbBackgroundImage == null) return NotFound();
                SectionBackgroundImageUpdateVM model = new()
                {
                    Value = dbBackgroundImage.Value
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
        public async Task<IActionResult> Edit(int? id,SectionBackgroundImageUpdateVM sectionBackgroundImage)
        {
            try
            {
                if (id == null) return BadRequest();
                SectionBackgroundImage dbBackgroundImage = await _backgroundImageService.GetSectionBackgroundImageByIdAsync(id);
                if (dbBackgroundImage == null) return NotFound();

                SectionBackgroundImageUpdateVM model = new()
                {
                    Value = dbBackgroundImage.Value
                };
                if (sectionBackgroundImage.Photo != null)
                {
                    if (!sectionBackgroundImage.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!sectionBackgroundImage.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbBackgroundImage.Value);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + sectionBackgroundImage.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, sectionBackgroundImage.Photo);
                    dbBackgroundImage.Value = fileName;
                }
                else
                {
                    SectionBackgroundImage newBackgroundImage = new()
                    {
                        Value = dbBackgroundImage.Value
                    };
                }
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
