using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class HeaderBackgroundService: IHeaderBackgroundService
    {
        private readonly AppDbContext _context;

        public HeaderBackgroundService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetHeaderBackgroundDatas() => _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        public async Task<List<HeaderBackground>> GetHeaderBackgroundsAsync() => await _context.HeaderBackgrounds.ToListAsync();
        public async Task<HeaderBackground> GetHeaderBackgroundByIdAsync(int? id) => await _context.HeaderBackgrounds.FirstOrDefaultAsync(m => m.Id == id);
    }
}
