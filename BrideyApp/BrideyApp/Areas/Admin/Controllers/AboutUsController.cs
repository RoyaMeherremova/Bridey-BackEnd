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
    public class AboutUsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAboutUsService _aboutUsService;
        public AboutUsController(AppDbContext context, IWebHostEnvironment env, IAboutUsService aboutUsService)
        {
            _context = context;
            _env = env;
            _aboutUsService = aboutUsService;
        }

        public async Task<IActionResult> Index()
        {
            List<AboutUs> aboutUss = await _aboutUsService.GetAll();
            return View(aboutUss);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            AboutUs aboutUs = await _aboutUsService.GetAboutUsById(id);
            if (aboutUs == null) return NotFound();
            return View(aboutUs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUsCreateVM aboutUs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(aboutUs);
                }

                if (!aboutUs.SmallPhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(aboutUs);
                }

                if (!aboutUs.SmallPhoto.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(aboutUs);

                }
                if (!aboutUs.LargePhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(aboutUs);
                }

                if (!aboutUs.LargePhoto.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(aboutUs);

                }

                string fileNameSmallPhoto = Guid.NewGuid().ToString() + " " + aboutUs.SmallPhoto.FileName;
                string newPathSmallPhoto = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileNameSmallPhoto);
                await FileHelper.SaveFileAsync(newPathSmallPhoto, aboutUs.SmallPhoto);
                string fileNameLargePhoto = Guid.NewGuid().ToString() + " " + aboutUs.LargePhoto.FileName;
                string newPathLargePhoto = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileNameLargePhoto);
                await FileHelper.SaveFileAsync(newPathLargePhoto, aboutUs.LargePhoto);

                AboutUs newAboutUs = new()
                {
                    SmallImage = fileNameSmallPhoto,
                    LargeImage = fileNameLargePhoto,
                    Title = aboutUs.Title,
                    Description = aboutUs.Description

                };
                await _context.AboutUss.AddAsync(newAboutUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {

                if (id == null) return BadRequest();
                AboutUs aboutUs = await _aboutUsService.GetAboutUsById(id);
                if (aboutUs == null) return NotFound();

                string pathSmallImg = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", aboutUs.SmallImage);
                string pathLargeImg = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", aboutUs.LargeImage);


                FileHelper.DeleteFile(pathSmallImg);
                FileHelper.DeleteFile(pathLargeImg);



                _context.AboutUss.Remove(aboutUs);
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
            if (id == null) return BadRequest();
            AboutUs aboutUs = await _aboutUsService.GetAboutUsById(id);
            if (aboutUs == null) return NotFound();

            AboutUsUpdateVM model = new()
            {
                SmallImage = aboutUs.SmallImage,
                LargeImage = aboutUs.LargeImage,
                Title = aboutUs.Title,
                Description = aboutUs.Description,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, AboutUsUpdateVM aboutUs)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutUs dbaboutUs= await _aboutUsService.GetAboutUsById(id);
                if (dbaboutUs == null) return NotFound();

                AboutUsUpdateVM model = new()
                {
                    SmallImage = dbaboutUs.SmallImage,
                    LargeImage = dbaboutUs.LargeImage,
                    Title = dbaboutUs.Title,
                    Description = dbaboutUs.Description,
                };
                if (aboutUs.SmallPhoto != null)
                {
                    if (!aboutUs.SmallPhoto.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(aboutUs);
                    }

                    if (!aboutUs.SmallPhoto.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(aboutUs);

                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbaboutUs.SmallImage);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + aboutUs.SmallPhoto.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, aboutUs.SmallPhoto);
                    dbaboutUs.SmallImage = fileName;
                }

                else
                {
                    AboutUs newAboutUs = new()
                    {
                        SmallImage = dbaboutUs.SmallImage
                    };
                }
                if (aboutUs.LargePhoto != null)
                {
                    if (!aboutUs.LargePhoto.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(aboutUs);
                    }

                    if (!aboutUs.LargePhoto.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(aboutUs);

                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbaboutUs.LargeImage);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + aboutUs.LargePhoto.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, aboutUs.LargePhoto);
                    dbaboutUs.LargeImage = fileName;
                }
                else
                {
                    AboutUs newAboutUs = new()
                    {
                        LargeImage = dbaboutUs.LargeImage
                    };
                }
                dbaboutUs.Title = aboutUs.Title;
                dbaboutUs.Description = aboutUs.Description;
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
