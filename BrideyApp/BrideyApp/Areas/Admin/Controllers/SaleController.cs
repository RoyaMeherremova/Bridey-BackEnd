using BrideyApp.Areas.Admin.ViewModels.Sale;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SaleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISaleService _saleService;
        public SaleController(AppDbContext context, IWebHostEnvironment env, ISaleService saleService)
        {
            _context = context;
            _env = env;
            _saleService = saleService;
        }
        public async Task<IActionResult> Index()
        {
            List<Sale> sales = await _saleService.GetAll();
            return View(sales);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Sale dbSale = await _saleService.GetSaleById(id);
                if (dbSale == null) return NotFound();

                SaleUpdateVM model = new()
                {
                    Image = dbSale.Image,
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
        public async Task<IActionResult> Edit(int? id, SaleUpdateVM sale)
        {
            try
            {
                if (id == null) return BadRequest();
                Sale dbSale = await _saleService.GetSaleById(id);
                if (dbSale == null) return NotFound();

                SaleUpdateVM model = new()
                {
                    Image = dbSale.Image,

                };
                if (sale.Photo != null)
                {
                    if (!sale.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!sale.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbSale.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + sale.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, sale.Photo);
                    dbSale.Image = fileName;
                }
                else
                {
                    Sale newSale = new()
                    {
                        Image = dbSale.Image
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
