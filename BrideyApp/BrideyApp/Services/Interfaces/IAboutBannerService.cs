using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IAboutBannerService
    {
        Task<List<AboutBanner>> GetAll();
        Task<AboutBanner> GetAboutBannerById(int? id);
    }
}
