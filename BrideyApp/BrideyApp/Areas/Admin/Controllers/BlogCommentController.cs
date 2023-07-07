using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BlogCommentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        public BlogCommentController(IBlogService blogService, AppDbContext context)
        {
            _blogService = blogService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _blogService.GetComments();
            return View(comments);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            BlogComment dbcomment = await _blogService.GetCommentById((int)id);
            if (dbcomment is null) return NotFound();

            _context.BlogComments.Remove(dbcomment);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            BlogComment dbcomment = await _blogService.GetCommentByIdWithBlog((int)id);
            if (dbcomment is null) return NotFound();
            return View(dbcomment);
        }

      
    }
}
