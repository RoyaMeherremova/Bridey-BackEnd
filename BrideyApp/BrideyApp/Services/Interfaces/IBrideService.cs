using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IBrideService
    {
        Task<List<Bride>> GetAll();
        Task<Bride> GetBrideById(int? id);
    }
}
