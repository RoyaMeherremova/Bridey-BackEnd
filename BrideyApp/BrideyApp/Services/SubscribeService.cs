using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class SubscribeService:ISubscribeService
    {
        private readonly AppDbContext _context;

        public SubscribeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Subscribe>> GetAll() => await _context.Subscribes.ToListAsync();

        public async Task<Subscribe> GetSubscribeById(int? id) => await _context.Subscribes.FirstOrDefaultAsync(m => m.Id == id);
    }
}
