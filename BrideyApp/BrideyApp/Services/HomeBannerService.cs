using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class HomeBannerService:IHomeBannerService
    {
        private readonly AppDbContext _context;

        public HomeBannerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<HomeBanner>> GetAll() => await _context.HomeBanners.ToListAsync();

        public async Task<HomeBanner> GetHomeBannerById(int? id) => await _context.HomeBanners.FirstOrDefaultAsync(m => m.Id == id);
    }
}
