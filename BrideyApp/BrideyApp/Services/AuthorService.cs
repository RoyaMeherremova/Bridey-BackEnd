using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class AuthorService:IAuthorService
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Author>> GetAll() => await _context.Authors.Include(m => m.Blogs).ToListAsync();
        public async Task<Author> GetById(int? id)=> await _context.Authors.FirstOrDefaultAsync(m => m.Id == id);
      
    }
}
