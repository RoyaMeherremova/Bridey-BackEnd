using BrideyApp.Areas.Admin.ViewModels.Bride;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBrideService _brideService;

        public BrideController(AppDbContext context,
                               IWebHostEnvironment env,
                               IBrideService brideService)
        {
            _context = context;
            _env = env;
            _brideService = brideService;
        }

        public async Task<IActionResult> Index()
        {
            List<Bride> brides = await _brideService.GetAll();
            return View(brides);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Bride bride = await _brideService.GetBrideById(id);
                if (bride == null) return NotFound();
                return View(bride);
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
        public async Task<IActionResult> Create(BrideCreateVM bride)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(bride);
                }

                if (!bride.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(bride);
                }

                if (!bride.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(bride);

                }

                string fileName = Guid.NewGuid().ToString() + " " + bride.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(newPath, bride.Photo);

                Bride newBride = new()
                {
                    Image = fileName,
                };
                await _context.Brides.AddAsync(newBride);
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
                Bride bride = await _brideService.GetBrideById(id);
                if (bride == null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", bride.Image);

                FileHelper.DeleteFile(path);


                _context.Brides.Remove(bride);
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
                Bride dbBride = await _brideService.GetBrideById(id);
                if (dbBride == null) return NotFound();

                BrideUpdateVM model = new()
                {
                    Image = dbBride.Image,
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
        public async Task<IActionResult> Edit(int? id, BrideUpdateVM bride)
        {
            try
            {
                if (id == null) return BadRequest();
                Bride dbBride = await _brideService.GetBrideById(id);
                if (dbBride == null) return NotFound();

                BrideUpdateVM model = new()
                {
                    Image = dbBride.Image,

                };
                if (bride.Photo != null)
                {
                    if (!bride.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!bride.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbBride.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + bride.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, bride.Photo);
                    dbBride.Image = fileName;
                }
                else
                {
                    Bride newBride = new()
                    {
                        Image = dbBride.Image
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
