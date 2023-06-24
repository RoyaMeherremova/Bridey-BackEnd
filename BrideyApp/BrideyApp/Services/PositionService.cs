using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class PositionService:IPositionService
    {
        private readonly AppDbContext _context;
        public PositionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Position>> GetAll() => await _context.Positions.ToListAsync();

        public async Task<Position> GetPositionById(int? id) => await _context.Positions.FirstOrDefaultAsync(m => m.Id == id);
    }
}
