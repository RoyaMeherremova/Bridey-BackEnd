using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ICompositionService
    {
        Task<List<Composition>> GetAll();
        Task<Composition> GetCompositionById(int? id);
    }
}
