using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;

        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Slider>> GetAll() => await _context.Sliders.ToListAsync();

        public async Task<Slider> GetSliderById(int? id) => await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

    }
}
