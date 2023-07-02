using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class CompositionService:ICompositionService
    {
        private readonly AppDbContext _context;
        public CompositionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Composition>> GetAll() => await _context.Compositions.Include(m => m.ProductCompositions).ToListAsync();

        public async Task<Composition> GetCompositionById(int? id) => await _context.Compositions.FirstOrDefaultAsync(m => m.Id == id);
    }
}
