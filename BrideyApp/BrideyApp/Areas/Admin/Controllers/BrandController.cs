using BrideyApp.Areas.Admin.ViewModels.Brand;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBrandService _brandService;
        public BrandController(AppDbContext context, IWebHostEnvironment env, IBrandService brandService)
        {
            _context = context;
            _env = env;
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            List<Brand> brands = await _brandService.GetAll();
            return View(brands);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Brand brand = await _brandService.GetBrandById(id);
                if (brand == null) return NotFound();
                return View(brand);
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
        public async Task<IActionResult> Create(BrandCreateVM brand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(brand);
                }

                if (!brand.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(brand);
                }

                if (!brand.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(brand);

                }

                string fileName = Guid.NewGuid().ToString() + " " + brand.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(newPath, brand.Photo);

                Brand newBrand = new()
                {
                    Image = fileName,
                    Name = brand.Name,

                };
                await _context.Brands.AddAsync(newBrand);
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
                Brand dbBrand = await _brandService.GetBrandById(id);
                if (dbBrand == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbBrand.Image);
                FileHelper.DeleteFile(path);
                _context.Brands.Remove(dbBrand);
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
                Brand dbBrand = await _brandService.GetBrandById(id);
                if (dbBrand == null) return NotFound();

                BrandUpdateVM model = new()
                {
                    Image = dbBrand.Image,
                    Name = dbBrand.Name,
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
        public async Task<IActionResult> Edit(int? id, BrandUpdateVM brand)
        {
            try
            {
                if (id == null) return BadRequest();
                Brand dbBrand = await _brandService.GetBrandById(id);
                if (dbBrand == null) return NotFound();

                BrandUpdateVM model = new()
                {
                    Image = dbBrand.Image,
                    Name = dbBrand.Name,
                };
                if (brand.Photo != null)
                {
                    if (!brand.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!brand.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbBrand.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + brand.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, brand.Photo);
                    dbBrand.Image = fileName;
                }
                else
                {
                    Brand newBrand = new()
                    {
                        Image = dbBrand.Image
                    };
                }

                dbBrand.Name = brand.Name;
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
