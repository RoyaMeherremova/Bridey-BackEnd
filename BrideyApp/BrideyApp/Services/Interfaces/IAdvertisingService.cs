using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IAdvertisingService
    {
        Task<List<Advertising>> GetAll();
        Task<Advertising> GetAdvertisingById(int? id);
    }
}
