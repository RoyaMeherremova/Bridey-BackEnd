using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<Subscribe>> GetAll();
        Task<Subscribe> GetSubscribeById(int? id);
    }
}
