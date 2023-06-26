using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ISocialService
    {
        Task<List<Social>> GetAll();
        Task<Social> GetSocialById(int? id);
    }
}
