using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class LayoutService:ILayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetSettingDatas() => _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        public async Task<List<Social>> GetAll() => await _context.Socials.ToListAsync();

    }
}
