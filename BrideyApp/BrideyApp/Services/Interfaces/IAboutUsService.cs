using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IAboutUsService
    {
        Task<List<AboutUs>> GetAll();
        Task<AboutUs> GetAboutUsById(int? id);
    }
}
