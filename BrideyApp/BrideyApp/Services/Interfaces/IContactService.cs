using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int? id);
    }
}
