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
    public class AboutBannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAboutBannerService _aboutBannerService;
        public AboutBannerController(AppDbContext context, 
                                     IWebHostEnvironment env,
                                     IAboutBannerService aboutBannerService)
        {
            _context = context;
            _env = env;
            _aboutBannerService = aboutBannerService;
        }

        public async Task<IActionResult> Index()
        {
            List<AboutBanner> aboutBanners = await _aboutBannerService.GetAll();
            return View(aboutBanners);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            AboutBanner aboutBanner = await _aboutBannerService.GetAboutBannerById(id);
            if (aboutBanner == null) return NotFound();
            return View(aboutBanner);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutBannerCreateVM aboutBanner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(aboutBanner);
                }

                if (!aboutBanner.SmallPhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(aboutBanner);
                }

                if (!aboutBanner.SmallPhoto.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(aboutBanner);

                }
                if (!aboutBanner.LargePhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(aboutBanner);
                }
                if (!aboutBanner.LargePhoto.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(aboutBanner);

                }

                string fileNameSmallPhoto = Guid.NewGuid().ToString() + " " + aboutBanner.SmallPhoto.FileName;
                string newPathSmallPhoto = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileNameSmallPhoto);
                await FileHelper.SaveFileAsync(newPathSmallPhoto, aboutBanner.SmallPhoto);
                string fileNameLargePhoto = Guid.NewGuid().ToString() + " " + aboutBanner.LargePhoto.FileName;
                string newPathLargePhoto = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileNameLargePhoto);
                await FileHelper.SaveFileAsync(newPathLargePhoto, aboutBanner.LargePhoto);

                AboutBanner newAboutBanner = new()
                {
                    SmallImage = fileNameSmallPhoto,
                    LargeImage = fileNameLargePhoto,
                    Title = aboutBanner.Title,
                    Description = aboutBanner.Description

                };
                await _context.AboutBanners.AddAsync(newAboutBanner);
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
                AboutBanner aboutBanner = await _aboutBannerService.GetAboutBannerById(id);
                if (aboutBanner == null) return NotFound();

                string pathSmallImg = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", aboutBanner.SmallImage);
                string pathLargeImg = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", aboutBanner.LargeImage);
                FileHelper.DeleteFile(pathSmallImg);
                FileHelper.DeleteFile(pathLargeImg);
                _context.AboutBanners.Remove(aboutBanner);
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
            AboutBanner dbaboutBanner = await _aboutBannerService.GetAboutBannerById(id);
            if (dbaboutBanner == null) return NotFound();

            AboutBannerUpdateVM model = new()
            {
                SmallImage = dbaboutBanner.SmallImage,
                LargeImage = dbaboutBanner.LargeImage,
                Title = dbaboutBanner.Title,
                Description = dbaboutBanner.Description,
            };
            return View(model);
        }
    }
}
