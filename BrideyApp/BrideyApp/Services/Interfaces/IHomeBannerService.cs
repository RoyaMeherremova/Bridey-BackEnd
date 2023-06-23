using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IHomeBannerService
    {
        Task<HomeBanner> GetHomeBannerById(int? id);
    }
}
