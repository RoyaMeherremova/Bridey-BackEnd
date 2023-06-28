using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IAboutBoxService
    {
        Task<List<AboutBox>> GetAll();
        Task<AboutBox> GetAboutBoxById(int? id);
    }
}
