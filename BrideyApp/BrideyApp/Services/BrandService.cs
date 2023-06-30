using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class BrandService:IBrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetAll() => await _context.Brands.ToListAsync();

        public async Task<Brand> GetBrandById(int? id) => await _context.Brands.FirstOrDefaultAsync(m => m.Id == id);
    }
}
