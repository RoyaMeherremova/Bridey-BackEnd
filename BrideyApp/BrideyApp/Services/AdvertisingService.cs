using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class AdvertisingService:IAdvertisingService
    {
        private readonly AppDbContext _context;

        public AdvertisingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Advertising>> GetAll() => await _context.Advertisings.ToListAsync();

        public async Task<Advertising> GetAdvertisingById(int? id) => await _context.Advertisings.FirstOrDefaultAsync(m => m.Id == id);
    }
}
