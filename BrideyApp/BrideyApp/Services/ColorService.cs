using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class ColorService:IColorService
    {
        private readonly AppDbContext _context;

        public ColorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Color>> GetAll() => await _context.Colors.Include(m=>m.ProductColors).ToListAsync();

        public async Task<Color> GetColorById(int? id) => await _context.Colors.FirstOrDefaultAsync(m => m.Id == id);
    }
}
