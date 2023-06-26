using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeBannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IHomeBannerService _homeBannnerService;
        public HomeBannerController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IHomeBannerService homeBannerService)
        {
            _context = context;
            _env = env;
            _homeBannnerService = homeBannerService;
        }
        public async Task<IActionResult> Index()
        {
            List<HomeBanner> homeBanners = await _homeBannnerService.GetAll();
            return View(homeBanners);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                HomeBanner homeBanner = await _homeBannnerService.GetHomeBannerById(id);
                if (homeBanner == null) return NotFound();
                return View(homeBanner);
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
        public async Task<IActionResult> Create(HomeBannerCreateVM homeBanner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(homeBanner);
                }

                if (!homeBanner.SmallPhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(homeBanner);
                }

                if (!homeBanner.SmallPhoto.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(homeBanner);

                }
                if (!homeBanner.LargePhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(homeBanner);
                }

                if (!homeBanner.LargePhoto.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(homeBanner);

                }

                string fileNameSmallPhoto = Guid.NewGuid().ToString() + " " + homeBanner.SmallPhoto.FileName;
                string newPathSmallPhoto = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileNameSmallPhoto);
                await FileHelper.SaveFileAsync(newPathSmallPhoto, homeBanner.SmallPhoto);
                string fileNameLargePhoto = Guid.NewGuid().ToString() + " " + homeBanner.LargePhoto.FileName;
                string newPathLargePhoto = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileNameLargePhoto);
                await FileHelper.SaveFileAsync(newPathLargePhoto, homeBanner.LargePhoto);

                HomeBanner newHomeBanner = new()
                {
                    SmallImage = fileNameSmallPhoto,
                    LargeImage = fileNameLargePhoto,
                    Title = homeBanner.Title,
                    Description = homeBanner.Description

                };
                await _context.HomeBanners.AddAsync(newHomeBanner);
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
                HomeBanner homeBanner = await _homeBannnerService.GetHomeBannerById(id);
                if (homeBanner == null) return NotFound();

                string pathSmallImg = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", homeBanner.SmallImage);
                string pathLargeImg = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", homeBanner.LargeImage);
                FileHelper.DeleteFile(pathSmallImg);
                FileHelper.DeleteFile(pathLargeImg);
                _context.HomeBanners.Remove(homeBanner);
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
                HomeBanner dbhomeBanner = await _homeBannnerService.GetHomeBannerById(id);
                if (dbhomeBanner == null) return NotFound();

                HomeBannerUpdateVM model = new()
                {
                    SmallImage = dbhomeBanner.SmallImage,
                    LargeImage = dbhomeBanner.LargeImage,
                    Title = dbhomeBanner.Title,
                    Description = dbhomeBanner.Description,
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
        public async Task<IActionResult> Edit(int? id, HomeBannerUpdateVM homeBanner)
        {
            try
            {
                if (id == null) return BadRequest();
                HomeBanner dbhomeBanner = await _homeBannnerService.GetHomeBannerById(id);
                if (dbhomeBanner == null) return NotFound();

                HomeBannerUpdateVM model = new()
                {
                    SmallImage = dbhomeBanner.SmallImage,
                    LargeImage = dbhomeBanner.LargeImage,
                    Title = dbhomeBanner.Title,
                    Description = dbhomeBanner.Description,
                };
                if (homeBanner.SmallPhoto != null)
                {
                    if (!homeBanner.SmallPhoto.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }

                    if (!homeBanner.SmallPhoto.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);

                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbhomeBanner.SmallImage);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + homeBanner.SmallPhoto.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, homeBanner.SmallPhoto);
                    dbhomeBanner.SmallImage = fileName;
                }

                else
                {
                    HomeBanner newHomeBanner = new()
                    {
                        SmallImage = dbhomeBanner.SmallImage
                    };
                }
                if (homeBanner.LargePhoto != null)
                {
                    if (!homeBanner.LargePhoto.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }

                    if (!homeBanner.LargePhoto.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);

                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbhomeBanner.LargeImage);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + homeBanner.LargePhoto.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, homeBanner.LargePhoto);
                    dbhomeBanner.LargeImage = fileName;
                }
                else
                {
                    HomeBanner newHomeBanner = new()
                    {
                        LargeImage = dbhomeBanner.LargeImage
                    };
                }
                dbhomeBanner.Title = homeBanner.Title;
                dbhomeBanner.Description = homeBanner.Description;
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
