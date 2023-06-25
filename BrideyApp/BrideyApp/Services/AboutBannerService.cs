using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class AboutBannerService:IAboutBannerService
    {
        private readonly AppDbContext _context;

        public AboutBannerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<AboutBanner>> GetAll() => await _context.AboutBanners.ToListAsync();

        public async Task<AboutBanner> GetAboutBannerById(int? id) => await _context.AboutBanners.FirstOrDefaultAsync(m => m.Id == id);
    }
}
