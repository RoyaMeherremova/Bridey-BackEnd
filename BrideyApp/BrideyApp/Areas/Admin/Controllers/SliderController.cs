using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;

        public SliderController(AppDbContext context, 
                                IWebHostEnvironment env,
                                ISliderService sliderService)
        {
            _context = context;
            _env = env;
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAll();
            return View(sliders);
        }
        //DETAIL
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
            Slider slider = await _sliderService.GetSliderById(id);

            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //CREATE SLIDER
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(slider);
                }

                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(slider);
                }

                if (!slider.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(slider);

                }

                string fileName = Guid.NewGuid().ToString() + " " + slider.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(newPath, slider.Photo);

                Slider newSlider = new()
                {
                    Image = fileName,
                    Header = slider.Header,
                    Title = slider.Title,
                    ShortDesc = slider.ShortDesc,
                    
                };
                await _context.Sliders.AddAsync(newSlider);
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

                Slider slider = await _sliderService.GetSliderById(id);

                if (slider == null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", slider.Image);

                FileHelper.DeleteFile(path);


                _context.Sliders.Remove(slider);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        //-----------UPDATE-----------

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Slider dbSlider = await _sliderService.GetSliderById(id);


            if (dbSlider == null) return NotFound();

            SliderUpdateVM model = new()
            {
                Image = dbSlider.Image,
                Header = dbSlider.Header,
                Title = dbSlider.Title,
                ShortDesc = dbSlider.ShortDesc,
            };
            return View(model);
        }

        //-----------UPDATE-----------
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, SliderUpdateVM slider)
        {
            try
            {
                if (id == null) return BadRequest();
                Slider dbSlider = await _sliderService.GetSliderById(id);
                if (dbSlider == null) return NotFound();

                SliderUpdateVM model = new()
                {
                    Image = dbSlider.Image,
                    Title = dbSlider.Title,
                    ShortDesc = dbSlider.ShortDesc,
                    Header = dbSlider.Header,
                };
                if (slider.Photo != null)
                {
                    if (!slider.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!slider.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbSlider.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + slider.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, slider.Photo);
                    dbSlider.Image = fileName;
                }
                else
                {
                    Slider newSlider = new()
                    {
                        Image = dbSlider.Image
                    };
                }

                dbSlider.Title = slider.Title;
                dbSlider.ShortDesc = slider.ShortDesc;
                dbSlider.Header = slider.Header;

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
