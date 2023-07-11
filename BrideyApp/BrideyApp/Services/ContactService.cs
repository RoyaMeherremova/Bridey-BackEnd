using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class ContactService:IContactService
    {
        private readonly AppDbContext _context;
        public ContactService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }
        public async Task<Contact> GetByIdAsync(int? id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
