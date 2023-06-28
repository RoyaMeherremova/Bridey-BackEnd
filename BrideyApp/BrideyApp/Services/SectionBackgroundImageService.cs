using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class SectionBackgroundImageService:ISectionBackgroundImageService
    {
        private readonly AppDbContext _context;

        public SectionBackgroundImageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SectionBackgroundImage>> GetSectionBackgroundImageDatasAsync() => await _context.SectionBackgroundImages.ToListAsync();
        public Dictionary<string, string> GetSectionBackgroundImages() => _context.SectionBackgroundImages.AsEnumerable().ToDictionary(s => s.Key, s => s.Value);
        public async Task<SectionBackgroundImage> GetSectionBackgroundImageByIdAsync(int? id) => await _context.SectionBackgroundImages.FirstOrDefaultAsync(sb => sb.Id == id);
    }
}
