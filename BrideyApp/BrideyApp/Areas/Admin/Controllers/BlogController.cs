using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogService;
        private readonly IAuthorService _authorService;

        public BlogController(AppDbContext context, IWebHostEnvironment env, IBlogService blogService, IAuthorService authorService)
        {
            _context = context;
            _env = env;
            _blogService = blogService;
            _authorService = authorService;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetAll();
            return View(blogs);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Blog blog = await _blogService.GetById(id);
                if (blog == null) return NotFound();
                return View(blog);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            @ViewBag.authors = await GetAuthorsAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blog)
        {
            try
            {
                @ViewBag.authors = await GetAuthorsAsync();

                if (!ModelState.IsValid)
                {
                    return View(blog);
                }

                if (!blog.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(blog);
                }

                if (!blog.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(blog);

                }

                string fileName = Guid.NewGuid().ToString() + " " + blog.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(newPath, blog.Photo);

                Blog newBlog = new()
                {
                    Image = fileName,
                    AuthorId = blog.AuthorId,
                    Title = blog.Title,
                    Description = blog.Description,

                };
                await _context.Blogs.AddAsync(newBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw;
            }

        }
        private async Task<SelectList> GetAuthorsAsync()
        {
            List<Author> authors = await _authorService.GetAll();
            return new SelectList(authors, "Id", "Name");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Blog blog = await _blogService.GetById(id);
                if (blog == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", blog.Image);
                FileHelper.DeleteFile(path);
                _context.Blogs.Remove(blog);
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
                @ViewBag.authors = await GetAuthorsAsync();
                if (id == null) return BadRequest();
                Blog dbBlog = await _blogService.GetById(id);
                if (dbBlog == null) return NotFound();

                BlogUpdateVM model = new()
                {
                    Image = dbBlog.Image,
                    Title = dbBlog.Title,
                    AuthorId= dbBlog.AuthorId,
                    Description = dbBlog.Description,
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
        public async Task<IActionResult> Edit(int? id, BlogUpdateVM blog)
        {
            try
            {
                @ViewBag.authors = await GetAuthorsAsync();

                if (id == null) return BadRequest();
                Blog dbBlog = await _blogService.GetById(id);
                if (dbBlog == null) return NotFound();

                BlogUpdateVM model = new()
                {
                    Image = dbBlog.Image,
                    Title = dbBlog.Title,
                    Description = dbBlog.Description,
                    AuthorId = dbBlog.AuthorId
                };
                if (blog.Photo != null)
                {
                    if (!blog.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!blog.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbBlog.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + blog.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, blog.Photo);
                    dbBlog.Image = fileName;
                }
                else
                {
                    Blog newBlog = new()
                    {
                        Image = dbBlog.Image
                    };
                }

                dbBlog.Title = blog.Title;
                dbBlog.Description = blog.Description;
                dbBlog.AuthorId = blog.AuthorId;

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
