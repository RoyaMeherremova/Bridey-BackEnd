using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ICompositionService _compositionService;
        private readonly IAdvertisingService _advertisingService;

        public BlogController(ILayoutService layoutService,
                              IBlogService blogService, 
                              ICategoryService categoryService,
                              ICompositionService compositionService,
                              IAdvertisingService advertisingService,
                              AppDbContext context)
        {
            _layoutService = layoutService;
            _blogService = blogService;
            _categoryService = categoryService;
            _compositionService = compositionService;
            _advertisingService = advertisingService;
            _context= context;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 4)
        {
            List<Blog> blogs = await _blogService.GetAll();
            List<Category> categories = await _categoryService.GetAll();
            List<Composition> compositions = await _compositionService.GetAll();
            List<Advertising> advertisings = await _advertisingService.GetAll();
            List<Blog> paginateBlogs = await _blogService.GetPaginatedDatas(page, take);
            int pageCount = await GetPageCountAsync(take);
            Paginate<Blog> paginatedDatas = new(paginateBlogs, page, pageCount);



            BlogVM model = new()
            {
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
                Blogs = blogs,
                Categories = categories,
                PaginatedDatas = paginatedDatas,
                Compositions = compositions,
                Advertisings= advertisings
            };
            return View(model);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            var blogCount = await _blogService.GetCountAsync();

            return (int)Math.Ceiling((decimal)blogCount / take);
        }


        [HttpGet]
        public async Task<IActionResult> BlogDetail(int? id)
        {
            if (id is null) return BadRequest();
            var dbBlog = await _blogService.GetById((int)id);
            if (dbBlog is null) return NotFound();
            var blogs = await _blogService.GetAll();

            BlogVM model = new()
            {
                Blog = dbBlog,
                Blogs = blogs.ToList(),
                Categories = await _categoryService.GetAll(),
                Compositions = await _compositionService.GetAll(),
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostComment(BlogVM model, int? id, string userId)
        {
            if (id is null || userId == null) return BadRequest();
            if (!ModelState.IsValid) return RedirectToAction(nameof(BlogDetail), new { id });

            BlogComment blogComment = new()
            {
                Name = model.BlogCommentVM.Name,
                Email = model.BlogCommentVM.Email,
                Message = model.BlogCommentVM.Message,
                AppUserId = userId,
                BlogId = (int)id
            };
            await _context.BlogComments.AddAsync(blogComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BlogDetail), new { id });
        }

    }
}
