using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IHomeBannerService
    {
        Task<List<HomeBanner>> GetAll();
        Task<HomeBanner> GetHomeBannerById(int? id);
    }
}
