using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class AboutBoxService:IAboutBoxService
    {
        private readonly AppDbContext _context;
        public AboutBoxService(AppDbContext context)
        {
            _context= context;  
        }
        public async Task<List<AboutBox>> GetAll() => await _context.AboutBoxes.ToListAsync();

        public async Task<AboutBox> GetAboutBoxById(int? id) => await _context.AboutBoxes.FirstOrDefaultAsync(m => m.Id == id);

    }
}
