using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class SaleService:ISaleService
    {
        private readonly AppDbContext _context;

        public SaleService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Sale>> GetAll() => await _context.Sales.ToListAsync();

        public async Task<Sale> GetSaleById(int? id) => await _context.Sales.FirstOrDefaultAsync(m => m.Id == id);
    }
}
