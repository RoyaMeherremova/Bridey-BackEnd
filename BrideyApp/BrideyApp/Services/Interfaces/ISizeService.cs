using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ISizeService
    {
        Task<List<Size>> GetAll();
        Task<Size> GetSizeById(int? id);
    }
}
