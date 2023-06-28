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
    public class AboutBoxController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAboutBoxService _aboutBoxService;

        public AboutBoxController(AppDbContext context, IWebHostEnvironment env, IAboutBoxService aboutBoxService)
        {
            _context = context;
            _env = env;
            _aboutBoxService = aboutBoxService;
        }
        public async Task<IActionResult> Index()
        {
            List<AboutBox> aboutBoxs = await _aboutBoxService.GetAll();
            return View(aboutBoxs);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutBox dbAboutBox = await _aboutBoxService.GetAboutBoxById(id);
                if (dbAboutBox == null) return NotFound();
                return View(dbAboutBox);
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
        public async Task<IActionResult> Create(AboutBoxCreateVM aboutBox)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(aboutBox);
                }

                if (!aboutBox.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(aboutBox);
                }

                if (!aboutBox.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(aboutBox);

                }

                string fileName = Guid.NewGuid().ToString() + " " + aboutBox.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(newPath, aboutBox.Photo);

                AboutBox newAboutBox = new()
                {
                    Image = fileName,
                    Title = aboutBox.Title,
                    Description = aboutBox.Description,
                };
                await _context.AboutBoxes.AddAsync(newAboutBox);
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
                AboutBox dbAboutBox = await _aboutBoxService.GetAboutBoxById(id);
                if (dbAboutBox == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbAboutBox.Image);
                FileHelper.DeleteFile(path);
                _context.AboutBoxes.Remove(dbAboutBox);
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
                AboutBox dbAboutBox = await _aboutBoxService.GetAboutBoxById(id);
                if (dbAboutBox == null) return NotFound();

                AboutBoxUpdateVM model = new()
                {
                    Image = dbAboutBox.Image,
                    Title = dbAboutBox.Title,
                    Description = dbAboutBox.Description,
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
        public async Task<IActionResult> Edit(int? id, AboutBoxUpdateVM aboutBox)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutBox dbAboutBox = await _aboutBoxService.GetAboutBoxById(id);
                if (dbAboutBox == null) return NotFound();

                AboutBoxUpdateVM model = new()
                {
                    Image = dbAboutBox.Image,
                    Title = dbAboutBox.Title,
                    Description = dbAboutBox.Description,
                };
                if (aboutBox.Photo != null)
                {
                    if (!aboutBox.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!aboutBox.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbAboutBox.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + aboutBox.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, aboutBox.Photo);
                    dbAboutBox.Image = fileName;
                }
                else
                {
                    AboutBox newAboutBox = new()
                    {
                        Image = dbAboutBox.Image
                    };
                }

                dbAboutBox.Title = aboutBox.Title;
                dbAboutBox.Description = aboutBox.Description;

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
