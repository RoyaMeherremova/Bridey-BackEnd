using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class BrideService:IBrideService
    {
        private readonly AppDbContext _context;
        public BrideService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Bride>> GetAll() => await _context.Brides.ToListAsync();

        public async Task<Bride> GetBrideById(int? id) => await _context.Brides.FirstOrDefaultAsync(m => m.Id == id);
    }
}
