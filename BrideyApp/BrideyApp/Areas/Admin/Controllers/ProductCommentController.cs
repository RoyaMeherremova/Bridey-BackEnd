using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCommentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;

        public ProductCommentController(AppDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _productService.GetComments();
            return View(comments);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ProductComment dbcomment = await _productService.GetCommentById((int)id);
            if (dbcomment is null) return NotFound();

            _context.ProductComments.Remove(dbcomment);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            ProductComment dbcomment = await _productService.GetCommentByIdWithProduct((int)id);
            if (dbcomment is null) return NotFound();
            return View(dbcomment);
        }

      
    }
}
