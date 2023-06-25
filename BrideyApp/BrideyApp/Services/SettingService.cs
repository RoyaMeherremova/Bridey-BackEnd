using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;

namespace BrideyApp.Services
{
    public class SettingService:ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Setting>> GetSettingsAsync() => await _context.Settings.ToListAsync();
        public async Task<Setting> GetSettingByIdAsync(int? id) => await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
    }
}
