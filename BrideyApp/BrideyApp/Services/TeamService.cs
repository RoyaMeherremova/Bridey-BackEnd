using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        public TeamService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Team>> GetAll() => await _context.Teams.Include(m=>m.Position).ToListAsync();
        public async Task<Team> GetTeamById(int? id) => await _context.Teams.Include(m => m.Position).FirstOrDefaultAsync(m => m.Id == id);
    }

}
