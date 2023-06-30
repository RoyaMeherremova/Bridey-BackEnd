using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class SizeService:ISizeService
    {
        private readonly AppDbContext _context;

        public SizeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Size>> GetAll() => await _context.Sizes.ToListAsync();

        public async Task<Size> GetSizeById(int? id) => await _context.Sizes.FirstOrDefaultAsync(m => m.Id == id);

     
    }
}
